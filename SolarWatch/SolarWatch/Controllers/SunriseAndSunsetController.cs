using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;

namespace SolarWatch.Controllers;

[Controller]
[Route("sunrise-and-sunset")]
public class SunriseAndSunsetController : ControllerBase
{
    private string APIKey = "https://api.sunrise-sunset.org/json";

    [HttpGet]
    public IActionResult GetByLocation([Required]float lng, [Required]float lat)
    {
        var Client = new WebClient();
        var timeData = Client.DownloadString($"{APIKey}?lat={lat}&lng={lng}");
        return Ok(ProcessJsonByLocation(timeData));
    }

    private static SunriseSunset ProcessJsonByLocation(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");

        SunriseSunset processedData = new SunriseSunset
        {
            SunriseTime = results.GetProperty("sunrise").GetString(),
            SunsetTime = results.GetProperty("sunset").GetString()
        };

        return processedData;
    }
}
