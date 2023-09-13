using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch.Controllers;

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

    
}