using Microsoft.EntityFrameworkCore;
using SolarWatch.Model;

namespace SolarWatch.DatabaseServices.Repositories;

public class SunriseSunsetRepository : ISunriseSunsetRepository
{
    private readonly IConfiguration _configuration;

    public SunriseSunsetRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<SunriseSunset> GetAll()
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        return dbContext.SunriseSunsets.ToList();
    }

    public SunriseSunset? GetById(int id)
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        return dbContext.SunriseSunsets
            .Include(sunset => sunset.City)
            .FirstOrDefault(sunset => sunset.Id == id);
    }

    public void Add(SunriseSunset sunriseSunset)
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        dbContext.SunriseSunsets.Add(sunriseSunset);
        dbContext.SaveChanges();
    }

    public void Delete(SunriseSunset sunriseSunset)
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        dbContext.Remove(sunriseSunset);
        dbContext.SaveChanges();
    }

    public void Update(SunriseSunset sunriseSunset)
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        dbContext.Update(sunriseSunset);
        dbContext.SaveChanges();
    }
}