using AutoMapper;
using LittleLemonAPI.Dto;
using LittleLemonAPI.Helper;
using LittleLemonAPI.Interfaces;
using LittleLemonAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleLemonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingTimesController : Controller
    {
        private readonly IbookingTimesRepository _BookingtimesRepository;
        private readonly IMapper _Mapper;
        private readonly ILogger _logger;


        public BookingTimesController(IbookingTimesRepository BookingTimesRepository, IMapper mapper, ILogger logger)
        {
            _BookingtimesRepository = BookingTimesRepository;
            _Mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingTimes>))]

        public async Task<IActionResult> GetBookingTimes([FromQuery] BookingTimeQueryObj query)
        {
            var bookingTimes = await _BookingtimesRepository.GetBookingTimes(query);
            var bookingTimesMap = _Mapper.Map<List<BookingTimesDto>>(bookingTimes);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bookingTimesMap);
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
            if (bookingTimes.Count == 0)
            {
                try
                {
                    string dateValue = DateTime.Parse(timeByDate).ToString("ddd");
                    bookingTimes = _Mapper.Map<List<BookingTimesDto>>(_BookingtimesRepository.GetBookingTimeByDate(dateValue));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "date conversion failed");
                }
            }

                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(bookingTimes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [Authorize]

        public async Task<IActionResult> CreateBookingTime(BookingTimesDto bookingTimes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // GetBookingTimes takes a query object but theres no need for query string params here
            BookingTimeQueryObj emptyQuery = new();
            var bookingTimeTask = await _BookingtimesRepository.GetBookingTimes(emptyQuery);

            var bookingTime = bookingTimeTask.Where(b => b.Time.Trim().ToUpper() == bookingTimes.Time.TrimEnd().ToUpper()
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
        [Authorize]

        public IActionResult DeleteBookingTime(int timeId) 
        { 
            if (!_BookingtimesRepository.TimeExists(timeId))
            {
                return NotFound();
            }

            var timeToDelete = _BookingtimesRepository.GetBookingTimeById(timeId)!;

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
        [Authorize]

        public IActionResult UpdateBookingTime(int timeId, [FromBody] BookingTimesDto updateTime)
        {
            if (!_BookingtimesRepository.TimeExists(timeId))
            {
                return NotFound();
            }

            var timeToUpdate = _BookingtimesRepository.GetBookingTimeById(timeId)!;

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
