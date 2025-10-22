var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("username", "admin");
var password = builder.AddParameter("password", "admin");

var keycloak = builder
    .AddKeycloak("keycloak", 8080, adminPassword: password)
    .WithImageTag("26.4.1")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithRealmImport("./keycloak/realms")
    .WithDataVolume()
    ;

var apiService = builder.AddProject<Projects.SampleAspNetCoreMcp_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.Build().Run();
