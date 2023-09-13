using dotenv.net;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch.Controllers;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;

namespace SolarWatchTest;

public class Tests
{
    private ApiController _controller;
    private Mock<ILogger<ApiController>> _loggerMock;
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<ApiController>>();
        _controller = new ApiController(_loggerMock.Object);
    }

    [Test]
    public void GetByCityNameAndDateReturnsTheRightData()
    {
        //Budapest coordinates:
        //"lat": ~47.4979937
        //"lon": ~19.0403594
        
        //Should return:
        //"sunrise": "4:17:12 AM"
        //"sunset": "5:02:50 PM"
        var result = _controller.GetByCityNameAndDate("Budapest", new DateOnly(2023, 09, 13));
        var okResult = result as OkObjectResult;
        Assert.That(okResult.Value, Is.EqualTo(new SunriseSunset{SunriseTime = "4:17:12 AM", SunsetTime = "5:02:50 PM"}));
    }
}