using ManageHotel.DTOs.Feedbacks;
using ManageHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _repository;
        public FeedbackController(IFeedbackRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllFeedback()
        {
            return Ok(_repository.GetAllFeedback());
        }

        [HttpGet("{hotelId}")]
        public IActionResult GetFeedbackByHotelId(int hotelId)
        {
            return Ok(_repository.GetFeedbackByHotelId(hotelId));
        }

        [HttpGet("{blogId}")]
        public IActionResult GetFeedbackByBlogId(int blogId)
        {
            return Ok(_repository.GetFeedbackByBlogId(blogId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddFeedback(AddFeedbackDTO dto)
        {
            _repository.CreateFeedback(dto);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateFeedback(int id, UpdateFeedbackDTO dto)
        {
            _repository.UpdateFeedback(id, dto);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteFeedback(int id)
        {
            _repository.DeleteFeedback(id);
            return Ok();
        }
    }
}
