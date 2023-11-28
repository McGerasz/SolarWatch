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

    public SolarWatchApiContext()
    {
        if(Database.IsRelational()) Database.Migrate();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing")
        {
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }
        else optionsBuilder.UseNpgsql(
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