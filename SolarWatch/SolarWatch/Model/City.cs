using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SolarWatch.Model;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string Name { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    [JsonIgnore]
    public ICollection<SunriseSunset> SunriseSunsets { get; } = new List<SunriseSunset>();

}