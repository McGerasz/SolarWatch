namespace SolarWatch.Services;

public class GeolocatorApi: IGeolocatorApi
{
    private readonly IConfiguration _config;
    private string GeolocatorBase = "http://api.openweathermap.org/geo/1.0/direct?q=";

    public GeolocatorApi(IConfiguration config)
    {
        _config = config;
    }
    public async Task<string> GetLocationData(string cityName)
    {
        var Client = new HttpClient();
        var coordData = await Client.GetStringAsync($"{GeolocatorBase}{cityName}&limit=1&appid={_config["GeolocatorAPI:APIKey"]}");
        return coordData;
    }
}