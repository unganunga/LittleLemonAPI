namespace LittleLemonAPI.Models
{
    public class TableBookings
    {
        public int Id { get; set; }

        public int BookingTimesId { get; set; }

        public BookingTimes BookingTimes { get; set; } = null!;

        public string Name { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;

        public string Occasion { get; set; } = string.Empty;
    }
}
