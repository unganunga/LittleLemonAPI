using LittleLemonAPI.Models;

namespace LittleLemonAPI.Dto
{
    public class TableBookingsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Occasion { get; set; }
    }
}
