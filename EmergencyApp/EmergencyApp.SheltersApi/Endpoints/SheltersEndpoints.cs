using System.ComponentModel;
using EmergencyApp.SheltersApi.Models;
using EmergencyApp.SheltersApi.Services;

namespace EmergencyApp.SheltersApi.Endpoints;

public static class SheltersEndpoints
{
    public static void MapShelterEndpoints(this WebApplication app)
    {
        app.MapGet("/api/shelters/nearest", async (
            [Description("Latitude of the search point in decimal degrees. Must be between -90 and 90.")]
            double lat,
            [Description("Longitude of the search point in decimal degrees. Must be between -180 and 180.")]
            double lon,
            ShelterQueryService shelterQuery,
            [Description("Number of nearest shelters to return. Defaults to 10. Maximum allowed value is 100.")]
            int count = 10) =>
        {
            if (lat < -90 || lat > 90)
                return Results.BadRequest("Latitude must be between -90 and 90.");
            if (lon < -180 || lon > 180)
                return Results.BadRequest("Longitude must be between -180 and 180.");
            if (count < 1 || count > 100)
                return Results.BadRequest("Count must be between 1 and 100.");

            var results = await shelterQuery.FindNearestAsync(lat, lon, count);
            return Results.Ok(results);
        })
        .WithName("GetNearestShelters")
        .WithSummary("Get the nearest bomb shelters to the given coordinates")
        .WithDescription("Returns shelters sorted by distance. Use the 'count' parameter to control how many are returned (default 10, max 100).")
        .Produces<List<ShelterDto>>()
        .ProducesProblem(400);
    }
}
