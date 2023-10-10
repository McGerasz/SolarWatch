using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SolarWatch.Model;

namespace SolarWatch.DatabaseServices;

public class SolarWatchApiContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SunriseSunset> SunriseSunsets { get; set; }
    private IConfiguration _configuration;

    public SolarWatchApiContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        if (databaseCreator != null)
        {
            if(!databaseCreator.CanConnect()) databaseCreator.Create();
            if(!databaseCreator.HasTables()) databaseCreator.CreateTables();
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            Environment.GetEnvironmentVariable("ASPNETCORE_CONNECTIONSTRING"));
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<City>()
            .HasIndex(u => u.Name)
            .IsUnique();
        builder.Entity<City>().HasMany(e => e.SunriseSunsets)
            .WithOne(e => e.City)
            .HasForeignKey(e => e.CityId)
            .HasPrincipalKey(e => e.Id);

    }
}