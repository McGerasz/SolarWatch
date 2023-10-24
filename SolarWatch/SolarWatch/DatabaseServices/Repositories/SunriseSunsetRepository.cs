using Microsoft.EntityFrameworkCore;
using SolarWatch.Model;

namespace SolarWatch.DatabaseServices.Repositories;

public class SunriseSunsetRepository : ISunriseSunsetRepository
{
    private readonly SolarWatchApiContext _context;

    public SunriseSunsetRepository(SolarWatchApiContext context)
    {
        _context = context;
    }
    public IEnumerable<SunriseSunset> GetAll()
    {
        return _context.SunriseSunsets.ToList();
    }

    public SunriseSunset? GetById(int id)
    {
        return _context.SunriseSunsets
            .Include(sunset => sunset.City)
            .FirstOrDefault(sunset => sunset.Id == id);
    }

    public void Add(SunriseSunset sunriseSunset)
    {
        _context.SunriseSunsets.Add(sunriseSunset);
        _context.SaveChanges();
    }

    public void Delete(SunriseSunset sunriseSunset)
    {
        _context.Remove(sunriseSunset);
        _context.SaveChanges();
    }

    public void Update(SunriseSunset sunriseSunset)
    {
        _context.Update(sunriseSunset);
        _context.SaveChanges();
    }
}