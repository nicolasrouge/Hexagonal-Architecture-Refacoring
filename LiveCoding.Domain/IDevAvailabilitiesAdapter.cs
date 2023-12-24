namespace LiveCoding.Domain;

public interface IDevAvailabilitiesAdapter
{
    List<DevAvailability> GetDevAvailabilities();
    int GetNumberOfDevs();
}