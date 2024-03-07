using LittleLemonAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LittleLemonAPI.Dto
{
    public class BookingTimesDto
    {
        public int Id { get; set; }

        [Required]
        public string Time { get; set; } = string.Empty;

        [Required]
        public string Date { get; set; } = string.Empty;
    }
}
