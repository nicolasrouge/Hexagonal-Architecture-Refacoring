using System.Net.NetworkInformation;
using LiveCoding.Persistence;

namespace LiveCoding.Services
{
    public class DevAvailability
    {
        public int NumberOfDevs { get; set; }
        public DateTime Day { get; }

        public DevAvailability(DateTime day, int numberOfDevs)
        {
            NumberOfDevs = numberOfDevs;
            Day = day;
        }
    }

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

            var devAvailabilities = GetDevAvailabilities(devs);

            var bestDate = BestDate.GetBestDate(devAvailabilities, devs.Count);

            if (bestDate is null) return false;

            foreach (var boatData in boats)
            {
                var bar = new Bar(boatData.MaxPeople, Enum.GetValues<DayOfWeek>(), boatData.Name);
                if (!bar.IsBookable(bestDate.NumberOfDevs, bestDate.Day)) continue;
                bar.BookBar(bestDate.Day);
                _bookingRepository.Save(new BookingData() { Bar = new BarData(boatData.Name, boatData.MaxPeople, Enum.GetValues<DayOfWeek>()), Date = bestDate.Day });
                return true;
            }

            foreach (var barData in bars)
            {
                var bar = new Bar(barData.Capacity, barData.Open, barData.Name);
                if (!bar.IsBookable(bestDate.NumberOfDevs, bestDate.Day)) continue;
                bar.BookBar(bestDate.Day);
                _bookingRepository.Save(new BookingData() { Bar = barData, Date = bestDate.Day });
                return true;
            }

            return false;
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