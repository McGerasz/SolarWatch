using System.ComponentModel.DataAnnotations.Schema;

namespace SolarWatch.Model;

public class City
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string Name { get; init; }
    public float Longitude { get; init; }
    public float Latitude { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public IEnumerable<SunriseSunset> SunriseSunsets { get; init; }
}