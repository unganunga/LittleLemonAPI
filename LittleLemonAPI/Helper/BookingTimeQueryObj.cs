using Microsoft.AspNetCore.Mvc.Rendering;

namespace LittleLemonAPI.Helper
{
    public class BookingTimeQueryObj
    {
        public string? Time { get; set; } = null;
        public string? Date { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 20;
        public int PageSize
        {
            get { return _pageSize; } 
            set
            {
                if (value > 20 || value < 1)
                {
                    _pageSize = 20;
                }
                else _pageSize = value;
                
            } }
    }
}
