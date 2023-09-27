using System.Runtime.InteropServices.JavaScript;
using SolarWatch.Model;

namespace SolarWatch.Extensions;

public static class CityExtensions
{
    public static SunriseSunset? GetBySSDate(this City city, DateOnly date)
    {
        try
        {
            Console.WriteLine(city.SunriseSunsets.First());
        return city.SunriseSunsets.FirstOrDefault(sunset => sunset.Date.Year == date.Year &&
                                                            sunset.Date.Month == date.Month
                                                            && sunset.Date.Day == date.Day);
        }
        catch
        {
            return null;
        }
    }
}