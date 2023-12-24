namespace LiveCoding.Domain;

public class BarNotFound : Bar
{
    public BarNotFound() : base(0, Array.Empty<DayOfWeek>(), string.Empty, false)
    {
    }
}