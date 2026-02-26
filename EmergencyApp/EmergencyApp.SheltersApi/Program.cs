using EmergencyApp.SheltersApi.Data;
using EmergencyApp.SheltersApi.Endpoints;
using EmergencyApp.SheltersApi.Services;
using EmergencyApp.SheltersApi.Tools;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Shelters API", Version = "v1", Description = "API for finding nearby bomb shelters in Kraków." });
});

var connectionString = builder.Configuration.GetConnectionString("SheltersDb")
    ?? "Data Source=shelters.db";

// When running in a container, the app directory is often read-only.
// Use the temp directory for the SQLite database to ensure it's writable.
if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    var dbPath = Path.Combine(Path.GetTempPath(), "shelters.db");
    connectionString = $"Data Source={dbPath}";
}

builder.Services.AddDbContext<SheltersDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<ShelterQueryService>();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools<ShelterTools>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shelters API v1");
    c.RoutePrefix = string.Empty; // serve Swagger UI at root "/"
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SheltersDbContext>();
    await ShelterSeeder.SeedAsync(db);
}

app.MapDefaultEndpoints();
app.MapShelterEndpoints();
app.MapMcp("/mcp");

app.Run();
