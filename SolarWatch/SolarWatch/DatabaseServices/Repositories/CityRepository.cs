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
        throw new NotImplementedException();
    }

    public City? GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public void Add(City city)
    {
        throw new NotImplementedException();
    }

    public void Delete(City city)
    {
        throw new NotImplementedException();
    }

    public void Update(City city)
    {
        throw new NotImplementedException();
    }
}