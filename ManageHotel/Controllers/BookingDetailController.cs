using ManageHotel.DTOs.BookingDetails;
using ManageHotel.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDetailController : ControllerBase
    {
        private readonly IBookingDetailRepository _repository;
        public BookingDetailController(IBookingDetailRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult CreateBookingDetail(List<AddBookingDetailDTO> dto)
        {
            _repository.CreateBookingDetail(dto);
            return Ok();
        }
    }
}
