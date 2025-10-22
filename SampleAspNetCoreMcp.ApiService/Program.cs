using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using ModelContextProtocol.AspNetCore.Authentication;
using SampleAspNetCoreMcp.ApiService.Tools;

var builder = WebApplication.CreateBuilder(args);

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
        options.MapInboundClaims = false;
        options.TokenValidationParameters.NameClaimType = "given_name";
    })
    .AddMcp(options =>
    {
        options.ResourceMetadata = new()
        {
            Resource = new Uri("http://localhost:5522"),
            ResourceDocumentation = new Uri("https://docs.example.com/api/math"),
            AuthorizationServers = { new Uri(builder.Configuration["Authentication:Schemes:Bearer:Authority"]!) },
            ScopesSupported = ["mcp:tools"]
        };
    })
    ;

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddMcpServer()
    .WithTools<MathTools>()
    .WithHttpTransport();

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// app.UseCors();

app.MapDefaultEndpoints();
app.MapMcp()
    .RequireAuthorization()
    ;

await app.RunAsync();