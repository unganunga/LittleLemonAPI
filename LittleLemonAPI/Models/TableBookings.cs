namespace LittleLemonAPI.Models
{
    public class TableBookings
    {
        public int Id { get; set; }

        public int BookingTimesId { get; set; }

        public BookingTimes BookingTimes { get; set; } = null!;

        public string Name { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Occasion { get; set; }
    }
}
