using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class JsonProcessor : IJsonProcessor
{
    public City ProcessCityData(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement result = json.RootElement;


        string name = result[0].GetProperty("name").GetString();
        string country = result[0].GetProperty("country").GetString();
        string state = "";
        try
        {
            result[0].GetProperty("state").GetString();
        }
        catch
        {
            state = "//";
        }
        double lon = result[0].GetProperty("lon").GetDouble();
        double lat = result[0].GetProperty("lat").GetDouble();
        

        return new City
        {
            Name = name,
            Country = country,
            State = state,
            Longitude = (float)lon,
            Latitude = (float)lat
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