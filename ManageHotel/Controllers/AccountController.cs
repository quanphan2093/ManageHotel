using ManageHotel.DTOs.Accounts;
using ManageHotel.Models;
using ManageHotel.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        public AccountController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllAccount());
        }

        [HttpPost]
        public IActionResult CreateAccount(AddAccountDTO dto)
        {
            _repository.CreateAccount(dto);
            return Ok();
        }

        [HttpGet]
        [Route("/Login")]
        public IActionResult Login(string email, string password)
        {
            return Ok(_repository.Login(email, password));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, UpdateAccountDTO dto)
        {
            _repository.UpdateAccount(id, dto);
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            _repository.DeleteAccount(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            return Ok(_repository.GetAccountById(id));
        }

        [HttpPost]
        [Route("/Register")]
        public IActionResult RegisterAccount(AddAccountDTO dto)
        {
            _repository.CreateAccount(dto);
            return Ok();
        }
    }
}
