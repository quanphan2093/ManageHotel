using ManageHotel.DTOs.Bookings;
using ManageHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllBooking()
        {
            return Ok(_repository.GetAllBooking());
        }

        [HttpPost]
        public IActionResult CreateBooking(AddBookingDTO dto)
        {
            return Ok(_repository.CreateBooking(dto));
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateBooking(int id, UpdateBookingDTO dTO)
        {
            _repository.UpdateBooking(id, dTO);
            return Ok();
        }
    }
}
