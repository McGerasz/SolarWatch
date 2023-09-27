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
            _configuration.GetConnectionString("MSSQLServer"));
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<City>()
            .HasIndex(u => u.Name)
            .IsUnique();
    }
}