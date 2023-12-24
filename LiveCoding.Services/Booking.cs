namespace LiveCoding.Services;

public static class Booking
{
    public static Bar? Book(List<Bar?> allBars, DevAvailability bestDate)
    {
        var orderedBars = allBars.OrderBy(bar => bar is { IsFavourite: true });

        var bar = orderedBars.FirstOrDefault(bar => bar != null && bar.IsBookable(bestDate.NumberOfDevs, bestDate.Day));

        return bar ?? new BarNotFound();
    }
}