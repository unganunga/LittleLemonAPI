using AutoMapper;
using LittleLemonAPI.Dto;
using LittleLemonAPI.Models;

namespace LittleLemonAPI.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<TableBookings, TableBookingsDto>();
            CreateMap<BookingTimes, BookingTimesDto>();
            CreateMap<TableBookingsDto, TableBookings>();
            CreateMap<BookingTimesDto, BookingTimes>();
        }
    }
}
