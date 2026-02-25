using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace EmergencyApp.Web.Services;

public class GoogleMapsService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;
    private const string StaticMapBaseUrl = "https://maps.googleapis.com/maps/api/staticmap";
    private const string PlaceLinkBaseUrl = "https://www.google.com/maps/search/?api=1&query=";
    private const string NavigationLinkBaseUrl = "https://www.google.com/maps/dir/?api=1";

    public GoogleMapsService()
    {
        _apiKey = "AIzaSyClfobnGLt3VhKkwMuMOz0SaukCoUhLuhQ";
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Creates a static image from a Google Map with multiple pins.
    /// </summary>
    /// <param name="coordinates">Collection of (Latitude, Longitude) tuples for pins.</param>
    /// <param name="width">Width of the image in pixels.</param>
    /// <param name="height">Height of the image in pixels.</param>
    /// <param name="zoom">Optional zoom level (0-21). If null, Google Maps will auto-fit the markers.</param>
    /// <returns>Byte array of the image.</returns>
    public async Task<byte[]> GetStaticMapImageAsync(IEnumerable<(double Lat, double Lng)> coordinates, int width = 600, int height = 400, int? zoom = null)
    {
        if (coordinates == null || !coordinates.Any())
        {
            throw new ArgumentException("At least one coordinate is required.", nameof(coordinates));
        }

        // Use %7C as the separator for markers (URL encoded pipe character)
        var markers = string.Join("%7C", coordinates.Select(c => $"{c.Lat},{c.Lng}"));
        
        // Construct the URL
        var url = $"{StaticMapBaseUrl}?size={width}x{height}&markers={markers}&key={Uri.EscapeDataString(_apiKey)}";
        
        if (zoom.HasValue)
        {
            url += $"&zoom={zoom.Value}";
        }

        // Fetch the image
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Google Maps Static API failed with status code {response.StatusCode}: {errorContent}");
        }

        return await response.Content.ReadAsByteArrayAsync();
    }

    /// <summary>
    /// Builds a Google Maps link to a specific place.
    /// </summary>
    /// <param name="lat">Latitude of the place.</param>
    /// <param name="lng">Longitude of the place.</param>
    /// <returns>Google Maps URL string.</returns>
    public string GetPlaceLink(double lat, double lng)
    {
        return $"{PlaceLinkBaseUrl}{lat},{lng}";
    }

    /// <summary>
    /// Builds a Google Maps navigation link from origin to destination.
    /// </summary>
    /// <param name="originLat">Latitude of the origin.</param>
    /// <param name="originLng">Longitude of the origin.</param>
    /// <param name="destLat">Latitude of the destination.</param>
    /// <param name="destLng">Longitude of the destination.</param>
    /// <returns>Google Maps navigation URL string.</returns>
    public string GetNavigationLink(double originLat, double originLng, double destLat, double destLng)
    {
        return $"{NavigationLinkBaseUrl}&origin={originLat},{originLng}&destination={destLat},{destLng}";
    }
}
