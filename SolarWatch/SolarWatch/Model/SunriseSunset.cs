using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace SolarWatch.Model;

public class SunriseSunset
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public string SunriseTime { get; set; }
    public string SunsetTime { get; set; }
    [Column(TypeName="date")]
    public DateTime Date { get; set; }
    public int CityId { get; set; }
    [JsonIgnore]
    public City City { get; set; }
}