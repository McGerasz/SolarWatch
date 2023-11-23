using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using SolarWatch.Contracts;
using SolarWatch.DatabaseServices.Repositories;
using SolarWatch.Model;

namespace SolarWatchIntegrationTests;

public class Tests
{
    private CustomWebApplicationFactory _factory;
    private HttpClient _client;
    [OneTimeSetUp]
    public void Setup()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
        Environment.SetEnvironmentVariable("ASPNETCORE_VALIDISSUER", "validIssuer");
        Environment.SetEnvironmentVariable("ASPNETCORE_VALIDAUDIENCE","validAudience");
        Environment.SetEnvironmentVariable("ASPNETCORE_ISSUERSIGNINGKEY", "issuerSigningKey");
        Environment.SetEnvironmentVariable("ASPNETCORE_ADMINEMAIL", "testing@test.com");
        Environment.SetEnvironmentVariable("ASPNETCORE_ADMINPASSWORD","Admin123");
        Environment.SetEnvironmentVariable("ASPNETCORE_GEOLOCATORAPIKEY", "b96dc248803eedf62185ecfbc1b9878f");
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task CityCRUDTests()
    {
        var response = await _client.PostAsync("api/SolarWatch/city?cityName=Kakucs", 
            JsonContent.Create(new object()));
        var getResponse = await _client.GetAsync("api/SolarWatch/city?id=1");
        string responseString = await getResponse.Content.ReadAsStringAsync();
        var city = JsonConvert.DeserializeObject<City>(responseString);
        Assert.That(city, Is.TypeOf(typeof(City)));

        var patchResponse = await _client.PatchAsync("api/SolarWatch/city?id=1&name=Budapest", null);
        var getResponseAfterPatch = await _client.GetAsync("api/SolarWatch/city?id=1");
        string responseAfterPatchString = await getResponseAfterPatch.Content.ReadAsStringAsync();
        var cityAfterPatch = JsonConvert.DeserializeObject<City>(responseAfterPatchString);
        Assert.That(cityAfterPatch.Name, Is.EqualTo("Budapest"));

        var deleteResponse = await _client.DeleteAsync("api/SolarWatch/city?id=1");
        var getResponseAfterDelete = await _client.GetAsync("api/SolarWatch/city?id=1");
        Assert.That(getResponseAfterDelete.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task SunriseSunsetCRUDTests()
    {
        //adding more cities to make sure there is no conflict between tests
        var postCityResponse = await _client.PostAsync("api/SolarWatch/city?cityName=Budapest", 
          null);
        var postCityResponse2 = await _client.PostAsync("api/SolarWatch/city?cityName=London", 
            null);
        var date = new DateTime();
        var postSunriseAndSunsetResponse = await _client.PostAsync($"api/SolarWatch/SunriseSunset?sunriseTime=5:00AM&sunsetTime=21:00PM&date={date.ToString()}&cityId=2", null);
        var getResponse = await _client.GetAsync("api/SolarWatch/SunriseSunset?id=1");
        string responseString = await getResponse.Content.ReadAsStringAsync();
        var snS = JsonConvert.DeserializeObject<SunriseSunset>(responseString);
        Assert.That(snS, Is.TypeOf(typeof(SunriseSunset)));

        var patchResponse = await _client.PatchAsync("api/SolarWatch/SunriseSunset?id=1&sunsetTime=8:00PM", null);
        var getResponseAfterPatch = await _client.GetAsync("api/SolarWatch/SunriseSunset?id=1");
        string responseStringAfterPatch = await getResponseAfterPatch.Content.ReadAsStringAsync();
        var snSAfterPatch = JsonConvert.DeserializeObject<SunriseSunset>(responseStringAfterPatch);
        Assert.That(snSAfterPatch.SunsetTime, Is.EqualTo("8:00PM"));
        var deleteResponse = await _client.DeleteAsync("api/SolarWatch/SunriseSunset?id=1");
        var getResponseAfterDelete = await _client.GetAsync("api/SolarWatch/SunriseSunset?id=1");
        Assert.That(getResponseAfterDelete.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        
    }
}