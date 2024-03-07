using LittleLemonAPI.Dto;
using LittleLemonAPI.Helper;
using LittleLemonAPI.Models;

namespace LittleLemonAPI.Interfaces
{
    public interface IbookingTimesRepository
    {
        Task<ICollection<BookingTimes>> GetBookingTimes(BookingTimeQueryObj query);

        BookingTimes? GetBookingTimeById(int id);

        BookingTimes? GetBookingTimeByTime(string time);

        ICollection<BookingTimes> GetBookingTimeByDate(string date);

        BookingTimes? GetBookingTime(string date, string time);

        bool TimeExists(int id);

        bool CreateBookingTime(BookingTimes bookingTime);

        bool UpdateBookingTime(BookingTimes bookingTime, BookingTimesDto updateTime);   

        bool DeleteBookingTime(BookingTimes bookingTime);

        bool Save();
    }
}
