using System.ComponentModel;
using System.Text.Json;
using EmergencyApp.SheltersApi.Services;
using ModelContextProtocol.Server;

namespace EmergencyApp.SheltersApi.Tools;

[McpServerToolType]
public class ShelterTools(ShelterQueryService shelterQuery)
{
    [McpServerTool]
    [Description("Find the nearest bomb shelters to a given geographic location. Returns shelters sorted by distance with their name, address, capacity and description.")]
    public async Task<string> FindNearestShelters(
        [Description("Latitude of the location in decimal degrees, between -90 and 90.")] double lat,
        [Description("Longitude of the location in decimal degrees, between -180 and 180.")] double lon,
        [Description("Maximum number of shelters to return. Defaults to 10, maximum 100.")] int count = 10)
    {
        var results = await shelterQuery.FindNearestAsync(lat, lon, count);
        return JsonSerializer.Serialize(results);
    }
}
