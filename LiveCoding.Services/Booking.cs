namespace LiveCoding.Services;

public static class Booking
{
    public static Bar? Book(List<Bar?> allBars, DevAvailability bestDate)
    {
        var orderedBars = allBars.OrderBy(b => b.IsFavourite);

        return orderedBars.FirstOrDefault(bar => bar != null && bar.IsBookable(bestDate.NumberOfDevs, bestDate.Day));
    }
}