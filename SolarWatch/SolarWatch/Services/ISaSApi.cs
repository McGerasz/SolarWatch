namespace SolarWatch.Services;

public interface ISaSApi
{
    Task<string> GetSunriseSunsetData(double lat, double lon, DateOnly dateOnly);
}