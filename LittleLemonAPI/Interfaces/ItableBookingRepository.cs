using LittleLemonAPI.Models;

namespace LittleLemonAPI.Interfaces
{
    public interface ItableBookingRepository
    {
        ICollection<TableBookings> GetTableBookings();

        TableBookings GetTableBooking(int id);

        TableBookings GetTableBooking(string name);

        TableBookings GetTableBookingByTime(string time);

        TableBookings GetTableBookingByDate(string date);

        bool HasTableBooking(int id);

        bool CreateTableBooking(TableBookings tableBooking);

        bool UpdateTableBooking(TableBookings tableBooking);

        bool DeleteTableBooking(int id);

        bool Save();
    }
}
