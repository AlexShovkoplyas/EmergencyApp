using System.ComponentModel;
using EmergencyApp.SheltersApi.Data;
using Microsoft.EntityFrameworkCore;

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
            SheltersDbContext db,
            [Description("Number of nearest shelters to return. Defaults to 10. Maximum allowed value is 100.")]
            int count = 10) =>
        {
            if (lat < -90 || lat > 90)
                return Results.BadRequest("Latitude must be between -90 and 90.");
            if (lon < -180 || lon > 180)
                return Results.BadRequest("Longitude must be between -180 and 180.");
            if (count < 1 || count > 100)
                return Results.BadRequest("Count must be between 1 and 100.");

            var shelters = await db.BombShelters.AsNoTracking().ToListAsync();

            var nearest = shelters
                .Select(s => new
                {
                    Shelter = s,
                    DistanceKm = HaversineDistance(lat, lon, s.Latitude, s.Longitude)
                })
                .OrderBy(x => x.DistanceKm)
                .Take(count)
                .Select(x => new ShelterDto(
                    x.Shelter.Id,
                    x.Shelter.Name,
                    x.Shelter.Address,
                    x.Shelter.Latitude,
                    x.Shelter.Longitude,
                    x.Shelter.Description,
                    x.Shelter.Capacity,
                    Math.Round(x.DistanceKm, 2)
                ))
                .ToList();

            return Results.Ok(nearest);
        })
        .WithName("GetNearestShelters")
        .WithSummary("Get the nearest bomb shelters to the given coordinates")
        .WithDescription("Returns shelters sorted by distance. Use the 'count' parameter to control how many are returned (default 10, max 100).")
        .Produces<List<ShelterDto>>()
        .ProducesProblem(400);
    }

    /// <summary>
    /// Haversine formula — returns great-circle distance in kilometres.
    /// </summary>
    private static double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371.0;
        var dLat = ToRad(lat2 - lat1);
        var dLon = ToRad(lon2 - lon1);
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
              + Math.Cos(ToRad(lat1)) * Math.Cos(ToRad(lat2))
              * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        return R * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    }

    private static double ToRad(double deg) => deg * Math.PI / 180.0;
}

public record ShelterDto(
    int Id,
    string Name,
    string Address,
    double Latitude,
    double Longitude,
    string Description,
    int Capacity,
    double DistanceKm
);
