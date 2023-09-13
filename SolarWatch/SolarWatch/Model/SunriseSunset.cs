namespace SolarWatch.Model;

public class SunriseSunset
{
    public string SunriseTime { get; set; }
    public string SunsetTime { get; set; }

    public override bool Equals(object obj)
    {
        if ((obj as SunriseSunset).SunriseTime == SunriseTime && (obj as SunriseSunset).SunsetTime == SunsetTime) return true;
        return false;
    }
}