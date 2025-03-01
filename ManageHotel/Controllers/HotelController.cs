using ManageHotel.DTOs.Hotels;
using ManageHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _repository;
        public HotelController(IHotelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllHotel()
        {
            return Ok(_repository.GetAllHotel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateHotel(AddHotelDTO dto)
        {
            _repository.CreateHotel(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateHotel(int id, UpdateHotelDTO dto)
        {
            _repository.UpdateHotel(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteHotel(int id)
        {
            _repository.DeleteHotel(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetHotelById(int id)
        {
            return Ok(_repository.GetHotelById(id));
        }
    }
}
