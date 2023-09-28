using System.Net;
using dotenv.net;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SolarWatch.DatabaseServices.Repositories;
using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatchTest;

public class Tests
{
    /*private ApiController _controller;
    private Mock<ILogger<ApiController>> _loggerMock;
    private IJsonProcessor _jsonProcessor;
    private ISaSApi _saSApi;
    private IGeolocatorApi _geolocatorApi;
    private ICityRepository _cityRepository;
    private ISunriseSunsetRepository _sunriseSunsetRepository;
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<ApiController>>();
        _jsonProcessor = new JsonProcessor();
        _saSApi = new SaSApi();
        _geolocatorApi = new GeolocatorApi();
        _cityRepository = new CityRepository(new Mock<IConfiguration>().Object);
        _sunriseSunsetRepository = new SunriseSunsetRepository(new Mock<IConfiguration>().Object);
        _controller = new ApiController(_loggerMock.Object, _jsonProcessor, _geolocatorApi, _saSApi, _cityRepository, _sunriseSunsetRepository);
    }

    [Test]
    public async Task EndpointReturnsOkObjectWithValue()
    {
        var response = await _controller.MainGet("Budapest", new DateOnly(2023, 09, 27));
        Assert.IsInstanceOf(typeof(OkObjectResult), response.Result);
        Assert.That(((OkObjectResult)response.Result).Value, Is.Not.Empty);
    }*/
}