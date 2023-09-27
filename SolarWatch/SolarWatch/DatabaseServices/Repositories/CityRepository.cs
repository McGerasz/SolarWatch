using Microsoft.EntityFrameworkCore;
using SolarWatch.Model;

namespace SolarWatch.DatabaseServices.Repositories;

public class CityRepository : ICityRepository
{
    private readonly IConfiguration _configuration;

    public CityRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<City> GetAll()
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        return dbContext.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        return dbContext.Cities.FirstOrDefault(c => c.Name == name);
    }

    public void Add(City city)
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        dbContext.Add(city);
        dbContext.SaveChanges();
    }

    public void Delete(City city)
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        dbContext.Remove(city);
        dbContext.SaveChanges();
    }

    public void Update(City city)
    {
        using var dbContext = new SolarWatchApiContext(_configuration);
        dbContext.Update(city);
        dbContext.SaveChanges();
    }
}