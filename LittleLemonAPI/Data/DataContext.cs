using LittleLemonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LittleLemonAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TableBookings> TableBookings { get; set; }

        public DbSet<BookingTimes> BookingTimes { get; set; }
    }
}
