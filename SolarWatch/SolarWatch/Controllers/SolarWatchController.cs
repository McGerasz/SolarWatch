using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using dotenv.net;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("get"), Authorize(Roles="User, Admin")]
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
            return Ok(searched);
        }
        catch (Exception e)
        {
            _logger.LogError("There was an error during operation");
            _logger.LogError((e.InnerException ?? e).Message);
            return Problem();
        }
    }

    [HttpGet("city"), Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> GetCity([Required] int id)
    {
        _logger.LogInformation("Searching for city in repository");
        City? city = _cityRepository.GetById(id);
        if (city is not null) return Ok(city);
        _logger.LogError("Id not found in city repository");
        return NotFound();
    }
    [HttpPost("city"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostCity([Required]string cityName)
    {
        _logger.LogInformation("Searching for city in repository");
        City? city = _cityRepository.GetByName(cityName);
        if (city is null)
        {
            _logger.LogInformation("City was not found");
            _logger.LogInformation("Fetching from external API...");
            var rawData = await _geolocatorApi.GetLocationData(cityName);
            _logger.LogInformation("Processing data...");
            city = _jsonProcessor.ProcessCityData(rawData);
            _logger.LogInformation("Updating database...");
            _cityRepository.Add(city);
            _logger.LogInformation("New City entry successfully added");
            return Ok(city);
        }
        _logger.LogError("City is already present in repository");
        return Conflict();
    }

    [HttpPatch("city"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PatchCity([Required] int id, string? name, float? lon, float? lat, string? state, string? country)
    {
        _logger.LogInformation("Searching for city in repository");
        City? city = _cityRepository.GetById(id);
        if (city is not null)
        {
            _logger.LogInformation("City was found in repository");
            _logger.LogInformation("Processing incoming updates...");
            city.Name = name ?? city.Name;
            city.Longitude = lon ?? city.Longitude;
            city.Latitude = lat ?? city.Latitude;
            if (state is not null)
            {
                if (state.Length == 2) city.State = state;
            }

            city.Country = country ?? city.Country;
            _logger.LogInformation("Updating database");
            _cityRepository.Update(city);
            _logger.LogInformation("Operation successful");
            return Ok(city);
        }
        _logger.LogError("City was not found in repository");
        return NotFound();
    }

    [HttpDelete("city"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCity([Required] int id)
    {
        _logger.LogInformation("Searching for city in repository");
        City? city = _cityRepository.GetById(id);
        if (city is not null)
        {
            _logger.LogInformation("City was found in repository");
            _logger.LogInformation("Deleting city from database...");
            _cityRepository.Delete(city);
            _logger.LogInformation("City deleted successfully");
            return Ok();
        }
        _logger.LogError("Id was not found in city repository");
        return NotFound();
    }

    [HttpGet("SunriseSunset"), Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> GetSnSById([Required] int id)
    {
        _logger.LogInformation("Searching for SunriseSunset in repository...");
        SunriseSunset? snS = _sunriseSunsetRepository.GetById(id);
        if (snS is not null) return Ok(snS);
        _logger.LogError("Id was not found in the repository");
        return NotFound();
    }
    [HttpPost("SunriseSunset"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostSnS([Required]string sunriseTime, [Required]string sunsetTime, [Required]DateTime date,
        [Required]int cityId)
    {
        _logger.LogInformation("Creating new object...");
        SunriseSunset newSnS = new SunriseSunset()
        {
            SunriseTime = sunriseTime,
            SunsetTime = sunsetTime,
            Date = date,
            CityId = cityId
        };
        _logger.LogInformation("Updating database...");
        _sunriseSunsetRepository.Add(newSnS);
        return Ok(_sunriseSunsetRepository.GetAll().First(sunset => sunset.SunriseTime == sunriseTime
                                                                    && sunset.SunsetTime == sunriseTime
                                                                    && sunset.Date.Year == date.Date.Year 
                                                                    && sunset.Date.Month == date.Date.Month
                                                                    && sunset.Date.Day == date.Date.Day
                                                                    && sunset.CityId == cityId));
    }

    [HttpPatch("SunriseSunset"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PatchSnS([Required] int id, string? sunriseTime, string? sunsetTime, DateTime? date,
        int? cityId)
    {
        _logger.LogInformation("Searching for SnS in repository");
        SunriseSunset? snS = _sunriseSunsetRepository.GetById(id);
        if (snS is not null)
        {
            _logger.LogInformation("SunriseSunset was found in repository");
            _logger.LogInformation("Processing incoming updates...");
            snS.SunriseTime = sunriseTime ?? snS.SunriseTime;
            snS.SunsetTime = sunsetTime ?? snS.SunsetTime;
            snS.Date = date ?? snS.Date;
            if (cityId is not null)
            {
                if (_cityRepository.GetById(cityId.Value) is not null)
                {
                    snS.CityId = (int)cityId;
                }
                _logger.LogError("CityId was not found in repository \n CityId will not be updated");
            }
            _logger.LogInformation("Updating database");
            _sunriseSunsetRepository.Update(snS);
            _logger.LogInformation("Operation successful");
            return Ok(snS);
        }
        _logger.LogError("SunriseSunset was not found in repository");
        return NotFound();
    }
    [HttpDelete("SunriseSunset"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSnS([Required] int id)
    {
        _logger.LogInformation("Searching for SunriseSUnet in repository");
        SunriseSunset? snS = _sunriseSunsetRepository.GetById(id);
        if (snS is not null)
        {
            _logger.LogInformation("SunriseSunset was found in repository");
            _logger.LogInformation("Deleting object from database...");
            _sunriseSunsetRepository.Delete(snS);
            _logger.LogInformation("Object deleted successfully");
            return Ok();
        }
        _logger.LogError("Id was not found in SunriseSunset repository");
        return NotFound();
    }
}
