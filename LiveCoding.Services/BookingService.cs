using LiveCoding.Domain;
using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IProvideBar _provideBar;
        private readonly IDevAvailabilitiesAdapter _devAvailabilitiesAdapter;

        public BookingService(
            IBookingRepository bookingRepository,
            IProvideBar provideBar, 
            IDevAvailabilitiesAdapter devAvailabilitiesAdapter)
        {
            _bookingRepository = bookingRepository;
            _provideBar = provideBar;
            _devAvailabilitiesAdapter = devAvailabilitiesAdapter;
        }

        public bool ReserveBar()
        {
            var allBars = _provideBar.GetAllBars();
            var devAvailabilities = _devAvailabilitiesAdapter.GetDevAvailabilities();
            var numberOfDevs = _devAvailabilitiesAdapter.GetNumberOfDevs();
            var bestDate = BestDate.GetBestDate(devAvailabilities, numberOfDevs);

            if (bestDate is DevAvailabilityNotFound) return false;

            var bookedBar = Booking.Book(allBars.ToList(), bestDate);
            if (bookedBar is BarNotFound) return false;

            _bookingRepository.Save(new BookingData() { Bar = new BarData(bookedBar.Name.Value, bookedBar.Capacity, Enum.GetValues<DayOfWeek>()), Date = bestDate.Day });

            return true;
        }
    }
}