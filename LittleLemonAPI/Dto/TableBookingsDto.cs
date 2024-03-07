using LittleLemonAPI.Models;

namespace LittleLemonAPI.Dto
{
    public class TableBookingsDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;

        public string Occasion { get; set; } = string.Empty;
    }
}
