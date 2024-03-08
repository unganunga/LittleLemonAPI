using LittleLemonAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LittleLemonAPI.Data
{
    public class DataContext: IdentityDbContext<Staff>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TableBookings> TableBookings { get; set; }

        public DbSet<BookingTimes> BookingTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roleList = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Name = "Staff",
                    NormalizedName = "STAFF"
                }
            };
            builder.Entity<IdentityRole>().HasData(roleList);
        }
    }
}
