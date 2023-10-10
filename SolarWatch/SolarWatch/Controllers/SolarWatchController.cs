using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using dotenv.net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SolarWatch.DatabaseServices.Repositories;
using SolarWatch.Extensions;
using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatch.Controllers;

[Controller]
[Route("/api/[controller]")]
public class SolarWatchController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IJsonProcessor _jsonProcessor;
    private readonly IGeolocatorApi _geolocatorApi;
    private readonly ISaSApi _saSApi;
    private readonly ICityRepository _cityRepository;
    private readonly ISunriseSunsetRepository _sunriseSunsetRepository;

    public SolarWatchController(ILogger<SolarWatchController> logger, IJsonProcessor jsonProcessor, IGeolocatorApi geolocatorApi,
        ISaSApi saSApi, ICityRepository cityRepository, ISunriseSunsetRepository sunriseSunsetRepository){
        _logger = logger;
        _jsonProcessor = jsonProcessor;
        _geolocatorApi = geolocatorApi;
        _saSApi = saSApi;
        _cityRepository = cityRepository;
        _sunriseSunsetRepository = sunriseSunsetRepository;
    }

    [HttpGet("get")]
    public async Task<ActionResult<SunriseSunset>> MainGet([Required] string cityName, [Required] DateOnly date)
    {
        _logger.LogInformation("Beginning operation");
        try
        {
            _logger.LogInformation("Searching for city in repository");
            City? city = _cityRepository.GetByName(cityName);
            if (city is null)
            {
                _logger.LogInformation("City was not found");
                _logger.LogInformation("Fetching from external API...");
                var rawData = await _geolocatorApi.GetLocationData(cityName);
                _logger.LogInformation(rawData.ToString());
                _logger.LogInformation("Processing data...");
                city = _jsonProcessor.ProcessCityData(rawData);
                _logger.LogInformation("Updating database...");
                _cityRepository.Add(city);
                city = _cityRepository.GetByName(cityName);
            }
            _logger.LogInformation("Searching for information about the date in database...");
            SunriseSunset? searched = city.GetBySSDate(date);
            if (searched is null)
            {
                _logger.LogInformation("Information not found");
                _logger.LogInformation("Fetching from external API...");
                var rawData = await _saSApi.GetSunriseSunsetData(city.Latitude, city.Longitude, date);
                _logger.LogInformation("Processing data...");
                var newSSData = _jsonProcessor.ProcessSunriseSunsetData(rawData);
                _logger.LogInformation("Fetch complete");
                _logger.LogInformation(newSSData.ToString());
                _logger.LogInformation("Updating database...");
                newSSData.CityId = city.Id;
                newSSData.Date = date.ToDateTime(TimeOnly.Parse("00:00 AM"));
                _sunriseSunsetRepository.Add(newSSData);
                city = _cityRepository.GetByName(cityName);
                searched = city.GetBySSDate(date);
            }
            _logger.LogInformation("Operation successful");
            return Ok($"{searched.SunriseTime}, {searched.SunsetTime}");
        }
        catch (Exception e)
        {
            _logger.LogError("There was an error during operation");
            _logger.LogError((e.InnerException ?? e).Message);
            return Problem();
        }
    }
}
