using ManageHotel.DTOs.Rooms;
using ManageHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _repository;
        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllRoom()
        {
            return Ok(_repository.GetAllRoom());
        }

        [HttpGet("/GetAllRoomUnBooking")]
        public IActionResult GetAllRoomUnBooking(DateTime to, DateTime from)
        {
            return Ok(_repository.GetAllRoomUnBooking(from, to));
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateRoom(AddRoomDTO dto)
        {
            _repository.CreateRoom(dto);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateRoom(int id, UpdateRoomDTO dto)
        {
            _repository.UpdateRoom(id, dto);    
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult DeleteRoom(int id)
        {
            _repository.DeleteRoom(id);
            return Ok();
        }
    }
}
