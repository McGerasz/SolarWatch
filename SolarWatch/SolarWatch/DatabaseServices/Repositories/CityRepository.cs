using Microsoft.EntityFrameworkCore;
using SolarWatch.Model;

namespace SolarWatch.DatabaseServices.Repositories;

public class CityRepository : ICityRepository
{
    private readonly SolarWatchApiContext _context;
    public CityRepository(SolarWatchApiContext context)
    {
        _context = context;
    }
    public IEnumerable<City> GetAll()
    {
        return _context.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        return _context.Cities
            .Include(city => city.SunriseSunsets)
            .FirstOrDefault(city => city.Name == name);
    }

    public void Add(City city)
    {
        _context.Add(city);
        _context.SaveChanges();
    }

    public void Delete(City city)
    {
        _context.Remove(city);
        _context.SaveChanges();
    }

    public void Update(City city)
    {
        _context.Update(city);
        _context.SaveChanges();
    }

    public City? GetById(int id)
    {
            return _context.Cities
                .Include(city => city.SunriseSunsets)
                .FirstOrDefault(city => city.Id == id);
    }
}