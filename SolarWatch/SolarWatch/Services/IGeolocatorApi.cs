namespace SolarWatch.Services;

public interface IGeolocatorApi
{
    Task<string> GetLocationData(string cityName);
}