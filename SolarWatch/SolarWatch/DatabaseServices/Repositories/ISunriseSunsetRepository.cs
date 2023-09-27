using SolarWatch.Model;

namespace SolarWatch.DatabaseServices.Repositories;

public interface ISunriseSunsetRepository
{
    IEnumerable<SunriseSunset> GetAll();
    SunriseSunset? GetById(int id);
    void Add(SunriseSunset sunriseSunset);
    void Delete(SunriseSunset sunriseSunset);
    void Update(SunriseSunset sunriseSunset);
}