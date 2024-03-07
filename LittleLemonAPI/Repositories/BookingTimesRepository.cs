using LittleLemonAPI.Data;
using LittleLemonAPI.Dto;
using LittleLemonAPI.Helper;
using LittleLemonAPI.Interfaces;
using LittleLemonAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        public BookingTimes? GetBookingTimeByTime(string time)
        {
            return _context.BookingTimes.Where(e => e.Time == time).FirstOrDefault();
        }

        public async Task<ICollection<BookingTimes>> GetBookingTimes(BookingTimeQueryObj query)
        {
            var times = _context.BookingTimes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Date)) 
            {
                times = times.Where(times => times.Date.Contains(query.Date));
            }

            if (!string.IsNullOrWhiteSpace(query.Time))
            {
                times = times.Where(times => times.Time.Contains(query.Time));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Time", StringComparison.OrdinalIgnoreCase))
                {
                    times = query.IsDecsending ? times.OrderByDescending(t => t.Time) : times.OrderBy(t => t.Time);
                }

                if (query.SortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    times = query.IsDecsending ? times.OrderByDescending(t => t.Time) : times.OrderBy(t => t.Time);
                }
            }

            var skipnumber = (query.PageIndex - 1) * query.PageSize;

            return await times.Skip(skipnumber).Take(query.PageSize).ToListAsync();
        }

        public BookingTimes? GetBookingTimeById(int id)
        {
            return _context.BookingTimes.Where(e => e.Id == id).FirstOrDefault();
        }

        public bool CreateBookingTime(BookingTimes bookingTime)
        {
            var bookingTimes = new BookingTimes()
            {
                Time = bookingTime.Time,
                Date = bookingTime.Date,
            };

            _context.Add(bookingTimes);

            return Save();
        }

        public bool UpdateBookingTime(BookingTimes bookingTime, BookingTimesDto updateTime)
        {
            bookingTime.Time = updateTime.Time;
            bookingTime.Date = updateTime.Date;

            return Save();
        }

        public bool DeleteBookingTime(BookingTimes bookingTime)
        {
            _context.Remove(bookingTime);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public BookingTimes? GetBookingTime(string date, string time)
        {
            return _context.BookingTimes.Where(e => e.Date == date && e.Time == time).FirstOrDefault();
        }
    }
}
