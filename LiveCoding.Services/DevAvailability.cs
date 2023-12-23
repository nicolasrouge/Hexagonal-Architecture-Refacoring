namespace LiveCoding.Services;

public class DevAvailability
{
    public int NumberOfDevs { get; set; }
    public DateTime Day { get; }

    public DevAvailability(DateTime day, int numberOfDevs)
    {
        NumberOfDevs = numberOfDevs;
        Day = day;
    }
}