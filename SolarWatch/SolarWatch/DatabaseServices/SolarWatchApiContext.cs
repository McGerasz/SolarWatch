using Microsoft.EntityFrameworkCore;
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
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            _configuration["MSSQLServer:ConnectionString"]);
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