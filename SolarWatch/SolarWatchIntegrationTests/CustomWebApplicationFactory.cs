using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using QuizWebAppIntegrationTest;
using SolarWatch.DatabaseServices.Repositories;

namespace SolarWatchIntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public CityRepository CityRepository { get; }
    public SunriseSunsetRepository SunriseSunsetRepository { get; }
    public SolarWatchApiTestContext TestDbContext { get; }
    

    public CustomWebApplicationFactory()
    {
        TestDbContext = new SolarWatchApiTestContext();
        CityRepository = new CityRepository(TestDbContext);
        SunriseSunsetRepository = new SunriseSunsetRepository(TestDbContext);
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton(CityRepository);

            services.AddSingleton(SunriseSunsetRepository);

            services.AddDbContext<SolarWatchApiTestContext>();
            
            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

        });

        builder.UseEnvironment("Testing");
    }
    
    
}