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
    [SetUp]
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
    [TearDown]
    public void OneTimeTearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task AdminCanLogin()
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
}