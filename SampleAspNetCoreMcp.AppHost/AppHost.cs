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
    .WithHttpHealthCheck("/health");

    var inspector = builder.AddMcpInspector("inspector", new McpInspectorOptions
    {
        InspectorVersion = "latest"
    })
    .WithEnvironment("ALLOWED_ORIGINS", "http://inspector-apphost.dev.localhost:6274,http://localhost:6274")
    .WithMcpServer(apiService);

builder.Build().Run();
