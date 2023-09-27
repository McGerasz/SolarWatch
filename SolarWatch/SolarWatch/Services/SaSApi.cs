namespace SolarWatch.Services;

public class SaSApi : ISaSApi
{
    private string SaSUrlBase = "https://api.sunrise-sunset.org/json";

    public async Task<string> GetSunriseSunsetData(float lat, float lon, DateOnly date)
    {
        var Client = new HttpClient();
        var timeData = await Client.GetStringAsync($"{SaSUrlBase}?lat={lat}&lng={lon}" +
                                                   $"&date={date.Year}-{date.Month}-{date.Day}");
        return timeData;
    }
}