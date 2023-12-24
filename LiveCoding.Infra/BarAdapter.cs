using LiveCoding.Domain;
using LiveCoding.Persistence;

namespace LiveCoding.Infra;

public class BarAdapter : IProvideBar
{
    private readonly IBarRepository _barRepo;
    private readonly IBoatRepository _boatRepo;

    public BarAdapter(IBarRepository barRepo, IBoatRepository boatRepo)
    {
        _barRepo = barRepo;
        _boatRepo = boatRepo;
    }

    public IEnumerable<Bar?> GetAllBars()
    {
        var bars = _barRepo.Get();
        var boats = _boatRepo.Get();

        var allBars = bars.Select(bar => new Bar(bar.Capacity, bar.Open, bar.Name, true)).ToList();
        allBars.AddRange(boats.Select(boat => new Bar(boat.MaxPeople, Enum.GetValues<DayOfWeek>(), boat.Name, false)));
        return allBars;
    }
}