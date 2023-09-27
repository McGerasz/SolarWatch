using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class JsonProcessor : IJsonProcessor
{
    public City ProcessCityData(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement jsonArray = json.RootElement;
        
        JsonElement firstItem = jsonArray.EnumerateArray().FirstOrDefault();

        string name = firstItem.GetProperty("name").GetString();
        string country = firstItem.GetProperty("country").GetString();
        string state = firstItem.GetProperty("state").GetString();
        double lon = firstItem.GetProperty("lon").GetDouble();
        double lat = firstItem.GetProperty("lat").GetDouble();

        return new City()
        {
            Name = name,
            Country = country,
            State = state,
            Longitude = (float)lon,
            Latitude = (float)lat,
            SunriseSunsets = new List<SunriseSunset>()
        };
    }

    public SunriseSunset ProcessSunriseSunsetData(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        
        return new SunriseSunset
        {
            SunriseTime = results.GetProperty("sunrise").GetString(),
            SunsetTime = results.GetProperty("sunset").GetString()
        };
    }
}