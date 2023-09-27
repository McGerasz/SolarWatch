namespace SolarWatch.Services;

public interface ISaSApi
{
    Task<string> GetSunriseSunsetData(float lat, float lon, DateOnly dateOnly);
}