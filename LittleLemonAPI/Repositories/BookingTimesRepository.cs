using LittleLemonAPI.Data;
using LittleLemonAPI.Interfaces;
using LittleLemonAPI.Models;

namespace LittleLemonAPI.Repositories
{
    public class BookingTimesRepository : IbookingTimesRepository
    {
        private DataContext _context;

        public BookingTimesRepository(DataContext context)
        {
            _context = context;
        }
        public bool TimeExists(int id)
        {
            return _context.BookingTimes.Any(t => t.Id == id);
        }

        public ICollection<BookingTimes> GetBookingTimeByDate(string date)
        {
            return _context.BookingTimes.Where(e => e.Date == date).ToList();
        }

        public BookingTimes GetBookingTimeByTime(string time)
        {
            return _context.BookingTimes.Where(e => e.Time == time).FirstOrDefault();
        }

        public ICollection<BookingTimes> GetBookingTimes()
        {
            return _context.BookingTimes.ToList();
        }

        public BookingTimes GetBookingTimeById(int id)
        {
            return _context.BookingTimes.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool CreateBookingTime(BookingTimes bookingTime)
        {
            _context.BookingTimes.Add(bookingTime);

            return Save();
        }

        public bool UpdateBookingTime(BookingTimes bookingTime)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBookingTime(BookingTimes bookingTime)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public BookingTimes GetBookingTime(string date, string time)
        {
            return _context.BookingTimes.Where(e => e.Date == date && e.Time == time).FirstOrDefault();
        }
    }
}
