using LiveCoding.Domain;
using LiveCoding.Persistence;

namespace LiveCoding.Infra;

public class DevAvailabilitiesAdapter : IDevAvailabilitiesAdapter
{
    private readonly IDevRepository _devRepo;

    public DevAvailabilitiesAdapter(IDevRepository devRepo)
    {
        _devRepo = devRepo;
    }

    public List<DevAvailability> GetDevAvailabilities()
    {
        var devs = _devRepo.Get();
        var devAvailabilities = new List<DevAvailability>();
        foreach (var date in devs.SelectMany(devData => devData.OnSite))
        {
            var devAvailability = devAvailabilities.FirstOrDefault(devAvailability => devAvailability.Day == date);
            if (devAvailability != null) devAvailability.NumberOfDevs++;
            else devAvailabilities.Add(new DevAvailability(date, 1));
        }
        return devAvailabilities;
    }

    public int GetNumberOfDevs()
    {
        return _devRepo.Get().ToList().Count();
    }
}