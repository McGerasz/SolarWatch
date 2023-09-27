using SolarWatch.Model;

namespace SolarWatch.Services;

public interface IJsonProcessor
{
    City ProcessCityData(string data);
    SunriseSunset ProcessSunriseSunsetData(string data);
}