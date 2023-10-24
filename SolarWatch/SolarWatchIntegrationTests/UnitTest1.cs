using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Moq;
using Newtonsoft.Json;
using SolarWatch.Contracts;
using SolarWatch.DatabaseServices.Repositories;
using SolarWatch.Model;

namespace SolarWatchIntegrationTests;

public class Tests
{
    //I have no idea whats going on but every like 5 minutes it changes what statuses i get from the tests
    private CustomWebApplicationFactory _factory;
    private HttpClient _client;
        
    private Mock<ICityRepository> _cityRepositoryMock;
    private Mock<ISunriseSunsetRepository> _sunriseRepositoryMock;
    private Mock<SolarWatchApiTestContext> _testDbContextMock;
    private string email = "testing2@test.com";
    private string password = "User123";
    private string username = "Testname";
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
        _cityRepositoryMock = new Mock<ICityRepository>();
        _sunriseRepositoryMock = new Mock<ISunriseSunsetRepository>();
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
    [Order(1)]
    public async Task MainGetTest()
    {
        var registerResponse = await _client.PostAsync("Auth/Register",
            new StringContent(JsonConvert.SerializeObject(new { email, username, password }), Encoding.UTF8,
                "application/json"));
        
        Assert.That(registerResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test]
    public async Task CityGetTest()
    {
        var registerResponse = await _client.PostAsync("Auth/Register",
            new StringContent(JsonConvert.SerializeObject(new { email, username, password }), Encoding.UTF8,
                "application/json"));
        var loginResponse = await _client.PostAsync("Auth/Login", new StringContent(JsonConvert.SerializeObject(new { email, password }), Encoding.UTF8, "application/json"));
        var responseContent = await loginResponse.Content.ReadAsStringAsync();
        var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseContent);
        var token = authResponse.Token;
        Assert.That(token, Is.Not.Null);
    }

    [Test]
    public async Task MainGetGivesUnauthorized()
    {
        var response = await _client.GetAsync("/api/SolarWatch/get?cityName=Budapest&date=2023.09.19");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task CityGetDoesntGiveUnauthorized()
    {
        var registerResponse = await _client.PostAsync("Auth/Register",
            new StringContent(JsonConvert.SerializeObject(new { email, username, password }), Encoding.UTF8,
                "application/json"));
        var loginResponse = await _client.PostAsync("Auth/Login", new StringContent(JsonConvert.SerializeObject(new { email, password }), Encoding.UTF8, "application/json"));
        var responseContent = await loginResponse.Content.ReadAsStringAsync();
        var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseContent);
        var token = authResponse.Token;
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var getResponse = await _client.GetAsync("/api/SolarWatch/city?id=1");
        Assert.That(getResponse.StatusCode, Is.Not.EqualTo(HttpStatusCode.Unauthorized));
    }
}