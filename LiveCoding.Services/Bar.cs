namespace LiveCoding.Services;

public class Bar
{
    public BarName Name { get; }
    public int Capacity { get; }
    public DayOfWeek[] Open { get; }
    public bool IsFavourite { get; }

    public Bar(int capacity, DayOfWeek[] open, string name, bool isFavourite)
    {
        Name = new BarName(name);
        Capacity = capacity;
        Open = open;
        IsFavourite = isFavourite;
    }

    public bool IsBookable(int maxNumberOfDevs, DateTime bestDate) =>
        HasEnoughCapacity(maxNumberOfDevs) && IsOpen(bestDate);

    public bool HasEnoughCapacity(int maxNumberOfDevs) => Capacity >= maxNumberOfDevs;

    public bool IsOpen(DateTime bestDate) => Open.Contains(bestDate.DayOfWeek);

    public void BookBar(DateTime dateTime) => Console.WriteLine("Bar booked: " + Name + " at " + dateTime);
}