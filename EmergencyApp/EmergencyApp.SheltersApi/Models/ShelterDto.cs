namespace EmergencyApp.SheltersApi.Models;

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
