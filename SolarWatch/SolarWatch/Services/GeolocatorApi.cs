namespace SolarWatch.Services;

public class GeolocatorApi: IGeolocatorApi
{
    private string GeolocatorBase = "http://api.openweathermap.org/geo/1.0/direct?q=";

    private string GeolocatorKey = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build()
        .GetSection("GeolocatorAPIKey").Value;
    public async Task<string> GetLocationData(string cityName)
    {
        var Client = new HttpClient();
        var coordData = await Client.GetStringAsync($"{GeolocatorBase}{cityName}&limit=1&appid={GeolocatorKey}");
        return coordData;
    }
}