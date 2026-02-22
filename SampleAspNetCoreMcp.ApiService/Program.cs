using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.AspNetCore.Authentication;
using SampleAspNetCoreMcp.ApiService.Data;
using SampleAspNetCoreMcp.ApiService.Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opts => opts.AddDefaultPolicy(policy => policy.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders | HttpLoggingFields.Duration;
    options.CombineLogs = true;
    options.RequestHeaders.Add("Authorization");
    options.ResponseHeaders.Add("WWW-Authenticate");
});

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = McpAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters.NameClaimType = ClaimTypes.GivenName;
    })
    .AddMcp(options =>
    {
        options.ResourceMetadata = new()
        {
            AuthorizationServers = { builder.Configuration["Authentication:Schemes:Bearer:Authority"]! },
            ScopesSupported = ["mcp:tools", "profile", "email", "roles"]
        };
    })
    ;

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseInMemoryDatabase("ToDoDb"));

builder.Services.AddMcpServer()
    .AddAuthorizationFilters()
    .WithToolsFromAssembly(typeof(CreateTodoTool).Assembly)
    .WithHttpTransport();

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();

app.MapMcp("/mcp")
    .RequireAuthorization()
    ;

await app.RunAsync();