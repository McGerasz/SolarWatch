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
    private readonly ILogger _logger;

    public SunriseAndSunsetController(ILogger<SunriseAndSunsetController> logger){
        _logger = logger;
    }
    [HttpGet]
    public IActionResult GetByLocation([Required]float lng, [Required]float lat)
    {
        try
        {
            _logger.LogInformation("Beginning GetByLocation operation");
            var Client = new WebClient();
            _logger.LogInformation("Downloading data from external API");
            var timeData = Client.DownloadString($"{APIKey}?lat={lat}&lng={lng}");
            _logger.LogInformation("Successfully downloaded data");
            return Ok(ProcessJsonByLocation(timeData));

        }
        catch(Exception e)
        {
            _logger.LogError("An error has occured while processing Your request");
            return BadRequest();
        }
    }

    private SunriseSunset ProcessJsonByLocation(string data)
    {
        _logger.LogInformation("Processing data");
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");
        
        SunriseSunset processedData = new SunriseSunset
        {
            SunriseTime = results.GetProperty("sunrise").GetString(),
            SunsetTime = results.GetProperty("sunset").GetString()
        };
        _logger.LogInformation("Data processed");
        return processedData;
    }
}
