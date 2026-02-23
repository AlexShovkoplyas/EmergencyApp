using EmergencyApp.SheltersApi.Data;
using EmergencyApp.SheltersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmergencyApp.SheltersApi.Services;

public class ShelterQueryService(SheltersDbContext db)
{
    public async Task<IReadOnlyList<ShelterDto>> FindNearestAsync(
        double lat, double lon, int count, CancellationToken cancellationToken = default)
    {
        count = Math.Clamp(count, 1, 100);

        var shelters = await db.BombShelters.AsNoTracking().ToListAsync(cancellationToken);

        return shelters
            .Select(s => new ShelterDto(
                s.Id, s.Name, s.Address,
                s.Latitude, s.Longitude,
                s.Description, s.Capacity,
                Math.Round(HaversineDistance(lat, lon, s.Latitude, s.Longitude), 2)))
            .OrderBy(s => s.DistanceKm)
            .Take(count)
            .ToList();
    }

    /// <summary>Haversine formula — returns great-circle distance in kilometres.</summary>
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
