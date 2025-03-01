using ManageHotel.DTOs.Blogs;
using ManageHotel.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _repository;
        public BlogController(IBlogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllBlog()
        {
            return Ok(_repository.GetAllBlog());
        }

        [HttpPost]
        public IActionResult CreateBlog(AddBlogDTO dto)
        {
            _repository.CreateBlog(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, UpdateBlogDTO dto)
        {
            _repository.UpdateBlog(id, dto);
            return Ok();    
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            _repository.DeleteBlog(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            _repository.GetBlogById(id);
            return Ok();
        }
    }
}
