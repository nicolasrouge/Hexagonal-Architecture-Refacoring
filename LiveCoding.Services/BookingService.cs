using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class BookingService
    {
        private readonly IDevRepository _devRepo;
        private readonly IBookingRepository _bookingRepository;
        private readonly IProvideBar _provideBar;

        public BookingService(
            IDevRepository devRepo,
            IBookingRepository bookingRepository,
            IProvideBar provideBar
        )
        {
            _devRepo = devRepo;
            _bookingRepository = bookingRepository;
            _provideBar = provideBar;
        }

        public bool ReserveBar()
        {
            var devs = _devRepo.Get().ToList();
            var allBars = _provideBar.GetAllBars();
            var devAvailabilities = GetDevAvailabilities(devs);
            var bestDate = BestDate.GetBestDate(devAvailabilities, devs.Count);

            if (bestDate is DevAvailabilityNotFound) return false;

            var bookedBar = Booking.Book(allBars.ToList(), bestDate);
            if (bookedBar is BarNotFound) return false;

            _bookingRepository.Save(new BookingData() { Bar = new BarData(bookedBar.Name.Value, bookedBar.Capacity, Enum.GetValues<DayOfWeek>()), Date = bestDate.Day });

            return true;
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