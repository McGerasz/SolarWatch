using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace SolarWatch.Model;

public class SunriseSunset
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string SunriseTime { get; init; }
    public string SunsetTime { get; init; }
    [Column(TypeName="date")]
    public DateTime Date { get; init; }
    public string CityName { get; init; }
}