namespace SolarWatch.Services;

public class GeolocatorApi: IGeolocatorApi
{
    private string GeolocatorBase = "http://api.openweathermap.org/geo/1.0/direct?q=";
    public async Task<string> GetLocationData(string cityName)
    {
        var Client = new HttpClient();
        var coordData = await Client.GetStringAsync($"{GeolocatorBase}{cityName}&limit=1&appid={Environment.GetEnvironmentVariable("ASPNETCORE_GEOLOCATORAPIKEY")}");
        return coordData;
    }
}