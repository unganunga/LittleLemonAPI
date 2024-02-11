using AutoMapper;
using LittleLemonAPI.Dto;
using LittleLemonAPI.Interfaces;
using LittleLemonAPI.Models;
using LittleLemonAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LittleLemonAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookingTimesController : Controller
    {
        private readonly IbookingTimesRepository _BookingtimesRepository;
        private readonly IMapper _Mapper;


        public BookingTimesController(IbookingTimesRepository BookingTimesRepository, IMapper mapper)
        {
            _BookingtimesRepository = BookingTimesRepository;
            _Mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingTimes>))]

        public IActionResult GetBookingTimes()
        {
            var bookingTimes = _Mapper.Map<List<BookingTimesDto>>(_BookingtimesRepository.GetBookingTimes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bookingTimes);
        }

        [HttpGet("{timeId}")]
        [ProducesResponseType(200, Type = typeof(BookingTimes))]
        [ProducesResponseType(400)]

        public IActionResult GetBookingTimeById(int timeId)
        {
            if (!_BookingtimesRepository.TimeExists(timeId))
            {
                return NotFound();
            }

            var bookingTime = _Mapper.Map<BookingTimesDto>(_BookingtimesRepository.GetBookingTimeById(timeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(bookingTime);
        }

        [HttpGet("time-by-date/{timeByDate}")]
        [ProducesResponseType(200, Type = typeof(BookingTimes))]
        [ProducesResponseType(400)]

        public IActionResult GetBookingTimeByDate(string timeByDate)
        {

            var bookingTimes = _Mapper.Map<List<BookingTimesDto>>(_BookingtimesRepository.GetBookingTimeByDate(timeByDate));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(bookingTimes);
        }

        
    }
}
