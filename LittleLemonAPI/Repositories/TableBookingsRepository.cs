using LittleLemonAPI.Data;
using LittleLemonAPI.Interfaces;
using LittleLemonAPI.Models;

namespace LittleLemonAPI.Repositories
{
    public class TableBookingsRepository : ItableBookingRepository
    {
        private readonly DataContext _context;
        public TableBookingsRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<TableBookings> GetTableBookings() 
        { 
            return _context.TableBookings.ToList();
        }

        public TableBookings GetTableBooking(int id)
        {
            return _context.TableBookings.Where(x => x.Id == id).FirstOrDefault();
        }

        public TableBookings GetTableBooking(string name) 
        {
            return _context.TableBookings.Where(x => x.Name == name).FirstOrDefault();
        }

        public TableBookings GetTableBookingByDate(string date)
        {
            return _context.TableBookings.Where(x => x.Date == date).FirstOrDefault();
        }

        public TableBookings GetTableBookingByTime(string time)
        {
            return _context.TableBookings.Where(x => x.Time == time).FirstOrDefault();
        }

        public bool HasTableBooking(int id)
        {
            return _context.TableBookings.Any(x => x.Id == id);
        }

        public bool CreateTableBooking(TableBookings tableBooking)
        {
            var tableBookings = new TableBookings()
            {
                Name = tableBooking.Name,
                Time = tableBooking.Time,
                Date = tableBooking.Date,
                Occasion = tableBooking.Occasion,
                BookingTimesId = tableBooking.BookingTimesId,
            };

            _context.Add(tableBookings);

            return Save();
        }

        public bool UpdateTableBooking(TableBookings tableBooking)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTableBooking(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
