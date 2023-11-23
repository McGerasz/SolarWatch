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
    private IJsonProcessor _jsonProcessor;
    [SetUp]
    public void SetUp()
    {
        _jsonProcessor = new JsonProcessor();
    }

    [Test]
    public void CityDataProcessingTest()
    {
        City city = _jsonProcessor.ProcessCityData("[\n{\n\"name\": \"Kakucs\",\n\"local_names\": {\n\"ru\": \"Какуч\",\n\"hu\": \"Kakucs\",\n\"bg\": \"Какуч\"\n},\n\"lat\": 47.2416179,\n\"lon\": 19.3667431,\n\"country\": \"HU\"\n}\n]");
        Assert.That(city.Name, Is.EqualTo("Kakucs"));
    }

    [Test]
    public void SunriseSunsetDataProcessingTest()
    {
        SunriseSunset snS = _jsonProcessor.ProcessSunriseSunsetData("{\n\"results\": {\n\"sunrise\": \"4:13:18 AM\",\n\"sunset\": \"5:08:52 PM\",\n\"solar_noon\": \"10:41:05 AM\",\n\"day_length\": \"12:55:34\",\n\"civil_twilight_begin\": \"3:43:59 AM\",\n\"civil_twilight_end\": \"5:38:11 PM\",\n\"nautical_twilight_begin\": \"3:07:05 AM\",\n\"nautical_twilight_end\": \"6:15:05 PM\",\n\"astronomical_twilight_begin\": \"2:28:17 AM\",\n\"astronomical_twilight_end\": \"6:53:52 PM\"\n},\n\"status\": \"OK\"\n}");
        Assert.That(snS.SunriseTime, Is.EqualTo("4:13:18 AM"));
    }
}