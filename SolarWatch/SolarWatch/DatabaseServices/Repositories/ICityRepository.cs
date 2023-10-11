using SolarWatch.Model;

namespace SolarWatch.DatabaseServices.Repositories;

public interface ICityRepository
{
    IEnumerable<City> GetAll();
    City? GetByName(string name);
    City? GetById(int id);

    void Add(City city);
    void Delete(City city);
    void Update(City city);
}