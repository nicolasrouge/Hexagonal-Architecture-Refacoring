namespace LiveCoding.Services;

public static class BestDate
{
    private const double minimumPercentageOfDevs = 0.6;

    public static DevAvailability GetBestDate(List<DevAvailability> devAvailabilities, int totalNUmberOfDevs)
    {
        var maxNumberOfDevs = GetMaxNumberOfDevs(devAvailabilities);

        var devAvailability = devAvailabilities.Find(devA => devA.NumberOfDevs == maxNumberOfDevs);
        //TODO: handle not found

        if (MinimumNumberOfDevsNotAvailable(maxNumberOfDevs, totalNUmberOfDevs))
        {
            return null;
        }
        else
        {
            return devAvailability; ;
        }
    }

    private static bool MinimumNumberOfDevsNotAvailable(int maxNumberOfDevs, int totalNumberOfDevs)
    {
        return maxNumberOfDevs <= totalNumberOfDevs * minimumPercentageOfDevs;
    }

    private static int GetMaxNumberOfDevs(IEnumerable<DevAvailability> devAvailabilities)
    {
        return devAvailabilities.Max(devAvailability => devAvailability.NumberOfDevs);
    }
}