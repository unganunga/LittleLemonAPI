namespace LittleLemonAPI.Models
{
    public class BookingTimes
    {
        public int Id { get; set; }

        public ICollection<TableBookings> tableBookings { get; set; }

        public string Time { get; set; }

        public string Date { get; set; }
    }
}
