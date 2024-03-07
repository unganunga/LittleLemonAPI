namespace LittleLemonAPI.Models
{
    public class BookingTimes
    {
        public int Id { get; set; }

        public ICollection<TableBookings> tableBookings { get; set; } = new List<TableBookings>();

        public string Time { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;
    }
}
