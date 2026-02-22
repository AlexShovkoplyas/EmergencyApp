using EmergencyApp.SheltersApi.Data;
using EmergencyApp.SheltersApi.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Shelters API", Version = "v1", Description = "API for finding nearby bomb shelters in Kraków." });
});

var connectionString = builder.Configuration.GetConnectionString("SheltersDb")
    ?? "Data Source=shelters.db";

builder.Services.AddDbContext<SheltersDbContext>(options =>
    options.UseSqlite(connectionString));

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

app.MapShelterEndpoints();

app.Run();
