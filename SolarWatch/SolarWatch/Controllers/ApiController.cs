using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;

namespace SolarWatch.Controllers;

[Controller]
[Route("/api")]
public class ApiController : ControllerBase
{
    private string SaSUrlBase = "https://api.sunrise-sunset.org/json";
    private string GeolocatorBase = "http://api.openweathermap.org/geo/1.0/direct?q=";

    private string GeolocatorKey = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build()
        .GetSection("GeolocatorAPIKey").Value;
    private readonly ILogger _logger;

    public ApiController(ILogger<ApiController> logger){
        _logger = logger;
    }
    /*[HttpGet("/api/getbylocation")]
    public async Task<ActionResult<SunriseSunset>> GetFromSunriseAndSunset([Required]float lng, [Required]float lat, [Required]DateOnly date)
    {
        try
        {
            _logger.LogInformation($"{lng} lng, {lat} lat");
            _logger.LogInformation("Beginning GetFromSunriseAndSunset operation");
            var Client = new HttpClient();
            _logger.LogInformation("Downloading data from external API");
            var timeData = await Client.GetStringAsync($"{SaSUrlBase}?lat={lat}&lng={lng}" +
                                                 $"&date={date.Year}-{date.Month}-{date.Day}");
            _logger.LogInformation("Successfully downloaded data");
            return Ok(ProcessJsonFromSnS(timeData));

        }
        catch(Exception e)
        {
            _logger.LogError("An error has occured while processing Your request");
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<ActionResult<SunriseSunset>> GetByCityNameAndDate([Required] string city, [Required] DateOnly date)
    {
        _logger.LogInformation("Beginning GetByCityNameAndDate operation");
        var Client = new HttpClient();
        _logger.LogInformation("Downloading data from external API");
        var coordData = await Client.GetStringAsync($"{GeolocatorBase}{city}&limit=1&appid={GeolocatorKey}");
        _logger.LogInformation("Successfully downloaded data");
        float[] coordinates = GetCoordinatesFromJson(coordData);
        var getSunriseAndSunsetData = GetFromSunriseAndSunset(coordinates[0], coordinates[1], date).Result;
        return Ok(((OkObjectResult)getSunriseAndSunsetData.Result).Value);
    }*/
}
