using Microsoft.EntityFrameworkCore;
using SolarWatch.DatabaseServices;

namespace SolarWatchIntegrationTests;

public class SolarWatchApiTestContext : SolarWatchApiContext
{
    public SolarWatchApiTestContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
    }
}