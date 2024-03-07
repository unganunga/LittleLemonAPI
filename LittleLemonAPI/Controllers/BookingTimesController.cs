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

        [HttpGet("{timeId:int}")]
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateBookingTime(BookingTimesDto bookingTimes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingTime = _BookingtimesRepository.GetBookingTimes()
                .Where(b => b.Time.Trim().ToUpper() == bookingTimes.Time.TrimEnd().ToUpper()
                && b.Date.Trim().ToUpper() == bookingTimes.Date.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (bookingTime != null)
            {
                ModelState.AddModelError("", "Time already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingTimeMap = _Mapper.Map<BookingTimes>(bookingTimes);

            if (!_BookingtimesRepository.CreateBookingTime(bookingTimeMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetBookingTimeById), new {timeId = bookingTimes.Id}, bookingTimes);
        }

        [HttpDelete("{timeId:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteBookingTime(int timeId) 
        { 
            if (!_BookingtimesRepository.TimeExists(timeId))
            {
                return NotFound();
            }

            var timeToDelete = _BookingtimesRepository.GetBookingTimeById(timeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_BookingtimesRepository.DeleteBookingTime(timeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting time");
            }

            return NoContent();
        }

        [HttpPut("{timeId:int}")]

        public IActionResult UpdateBookingTime(int timeId, [FromBody] BookingTimesDto updateTime)
        {
            if (!_BookingtimesRepository.TimeExists(timeId))
            {
                return NotFound();
            }

            var timeToUpdate = _BookingtimesRepository.GetBookingTimeById(timeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_BookingtimesRepository.UpdateBookingTime(timeToUpdate, updateTime))
            {
                ModelState.AddModelError("", "Something went wrong while updating time");
            }

            var bookingTimeMap = _Mapper.Map<BookingTimesDto>(timeToUpdate);

            return Ok(bookingTimeMap);
        }
    }
}
