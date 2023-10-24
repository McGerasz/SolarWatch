using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SolarWatch.DatabaseServices.Repositories;

namespace SolarWatchIntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public Mock<ICityRepository> CityRepositoryMock { get; }
    public CityRepository CityRepository { get; }
    public Mock<ISunriseSunsetRepository> SunriseSunsetRepositoryMock { get; }
    public SunriseSunsetRepository SunriseSunsetRepository { get; }
    public SolarWatchApiTestContext TestDbContext { get; }
    

    public CustomWebApplicationFactory()
    {
        CityRepositoryMock = new Mock<ICityRepository>();
        SunriseSunsetRepositoryMock = new Mock<ISunriseSunsetRepository>();
        TestDbContext = new SolarWatchApiTestContext();
        CityRepository = new CityRepository(TestDbContext);
        SunriseSunsetRepository = new SunriseSunsetRepository(TestDbContext);

    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton(CityRepositoryMock.Object);

            services.AddSingleton(SunriseSunsetRepositoryMock.Object);

            services.AddSingleton(CityRepository);

            services.AddSingleton(SunriseSunsetRepository);

            services.AddDbContext<SolarWatchApiTestContext>();

        });

        builder.UseEnvironment("Testing");
    }
    
    
}