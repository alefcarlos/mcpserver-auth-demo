#:sdk Aspire.AppHost.Sdk@13.1.1
#:package Aspire.Hosting.Keycloak
#:package CommunityToolkit.Aspire.Hosting.McpInspector
#:property OutputType=Exe
#:property TargetFramework=net10.0
#:property UserSecretsId=6ee153c9-74d0-4722-8749-cbff4a8e50a6
#:project SampleAspNetCoreMcp.ApiService/SampleAspNetCoreMcp.ApiService.csproj

var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", "admin");
var password = builder.AddParameter("password", "admin");

var keycloak = builder
    .AddKeycloak("keycloak", 8080, adminPassword: password)
    .WithImageTag("26.5.4")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithRealmImport("./keycloak/realms")
    .WithDataVolume()
    ;

var apiService = builder.AddProject<Projects.SampleAspNetCoreMcp_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WaitFor(keycloak);

var inspector = builder.AddMcpInspector("inspector", new McpInspectorOptions
{
    InspectorVersion = "latest"
})
.WithEnvironment("ALLOWED_ORIGINS", "http://inspector-apphost.dev.localhost:6274,http://localhost:6274")
.WithMcpServer(apiService)
.WaitFor(apiService);

builder.Build().Run();
