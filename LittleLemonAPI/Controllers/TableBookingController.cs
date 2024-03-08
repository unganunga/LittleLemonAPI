using AutoMapper;
using LittleLemonAPI.Dto;
using LittleLemonAPI.Interfaces;
using LittleLemonAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LittleLemonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableBookingController : ControllerBase
    {
        private readonly IbookingTimesRepository _BookingTimesRepository;
        private readonly ItableBookingRepository _TableBookingRepository;
        private readonly IMapper _Mapper;

        public TableBookingController(ItableBookingRepository bookingRepository, 
            IbookingTimesRepository bookingTimesRepository, 
            IMapper mapper)
        {
            _BookingTimesRepository = bookingTimesRepository;
            _TableBookingRepository = bookingRepository;
            _Mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TableBookings>))]
        [Authorize]

        public IActionResult GetTableBookings()
        {
            var tableBookings = _Mapper.Map<List<TableBookingsDto>>(_TableBookingRepository.GetTableBookings());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(tableBookings);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TableBookings))]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult GetTableBooking(int id)
        {
            if (!_TableBookingRepository.HasTableBooking(id))
            {
                return NotFound();
            }

            var tableBooking = _Mapper.Map<List<TableBookingsDto>>(_TableBookingRepository.GetTableBooking(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tableBooking);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTableBooking([FromBody] TableBookingsDto tableBookingCreate)
        {
            if (!ModelState.IsValid) 
            { return BadRequest(ModelState); }

            var tableBooking = _TableBookingRepository.GetTableBookings()
                .Where(b => b.Name.Trim().ToUpper() == tableBookingCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();

            if (tableBooking != null)
            {
                ModelState.AddModelError("", "Booking already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tableBookingMap = _Mapper.Map<TableBookings>(tableBookingCreate);

            var bookingTime = _BookingTimesRepository.GetBookingTime(tableBookingCreate.Date, tableBookingCreate.Time);

            if (bookingTime != null) 
            {
                tableBookingMap.BookingTimesId = bookingTime.Id;
            }
            else 
            { 
                string dateValue = DateTime.Parse(tableBookingCreate.Date, CultureInfo.InvariantCulture).ToString("ddd");
                bookingTime = _BookingTimesRepository.GetBookingTime(dateValue, tableBookingCreate.Time);

                if (bookingTime != null)
                {
                    tableBookingMap.BookingTimesId = bookingTime.Id;
                }
            }


            if (!_TableBookingRepository.CreateTableBooking(tableBookingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{bookingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult DeleteBookingTime(int bookingId)
        {
            if (!_TableBookingRepository.HasTableBooking(bookingId))
            {
                return NotFound();
            }

            var bookingToDelete = _TableBookingRepository.GetTableBooking(bookingId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_TableBookingRepository.DeleteTableBooking(bookingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting time");
            }

            return NoContent();
        }
    }
}
