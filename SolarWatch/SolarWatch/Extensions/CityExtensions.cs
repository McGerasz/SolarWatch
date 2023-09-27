using System.Runtime.InteropServices.JavaScript;
using SolarWatch.Model;

namespace SolarWatch.Extensions;

public static class CityExtensions
{
    public static SunriseSunset? HasDate(this City city, DateTime date)
    {
        return city.SunriseSunsets.FirstOrDefault(sunset => sunset.Date.Year == date.Year &&
                                                            sunset.Date.Month == date.Month
                                                            && sunset.Date.Day == date.Day);
    }
}