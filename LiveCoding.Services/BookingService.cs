using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class BookingService
    {
        private readonly IBarRepository _barRepo;
        private readonly IDevRepository _devRepo;
        private readonly IBoatRepository _boatRepo;
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBarRepository barRepo,
            IDevRepository devRepo,
            IBoatRepository boatRepo,
            IBookingRepository bookingRepository
        )
        {
            _barRepo = barRepo;
            _devRepo = devRepo;
            _boatRepo = boatRepo;
            _bookingRepository = bookingRepository;
        }

        public bool ReserveBar()
        {
            var bars = _barRepo.Get();
            var devs = _devRepo.Get().ToList();
            var boats = _boatRepo.Get();

            var allBars = GetAllBars(bars, boats);
            var devAvailabilities = GetDevAvailabilities(devs);
            var bestDate = BestDate.GetBestDate(devAvailabilities, devs.Count);

            if (bestDate is DevAvailabilityNotFound) return false;

            var bookedBar = Booking.Book(allBars.ToList(), bestDate);
            if (bookedBar is BarNotFound) return false;

            _bookingRepository.Save(new BookingData() { Bar = new BarData(bookedBar.Name.Value, bookedBar.Capacity, Enum.GetValues<DayOfWeek>()), Date = bestDate.Day });

            return true;
        }

        private static IEnumerable<Bar?> GetAllBars(IEnumerable<BarData> bars, IEnumerable<BoatData> boats)
        {
            var allBars = bars.Select(bar => new Bar(bar.Capacity, bar.Open, bar.Name, true)).ToList();
            allBars.AddRange(boats.Select(boat => new Bar(boat.MaxPeople, Enum.GetValues<DayOfWeek>(), boat.Name, false)));
            return allBars;
        }

        private static List<DevAvailability> GetDevAvailabilities(IEnumerable<DevData> devs)
        {
            var devAvailabilities = new List<DevAvailability>();
            foreach (var date in devs.SelectMany(devData => devData.OnSite))
            {
                var devAvailability = devAvailabilities.FirstOrDefault(devAvailability => devAvailability.Day == date);
                if (devAvailability != null) devAvailability.NumberOfDevs++;
                else devAvailabilities.Add(new DevAvailability(date, 1));
            }
            return devAvailabilities;
        }
    }
}