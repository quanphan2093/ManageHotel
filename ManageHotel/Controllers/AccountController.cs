using ManageHotel.Config;
using ManageHotel.DTOs.Accounts;
using ManageHotel.Models;
using ManageHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly EmailService _emailConfig;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountRepository repository, IConfiguration configuration, EmailService emailConfig)
        {
            _repository = repository;
            _configuration = configuration;
            _emailConfig = emailConfig;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
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
        [Route("Login")]
        public IActionResult Login([FromQuery] string email, [FromQuery] string password)
        {
            var user = _repository.Login(email, password);
            if (user != null || user.IsDeleted==false)
            {
                var issuer = _configuration["jwt:issuer"];
                var audience = _configuration["jwt:audience"];
                var key = Encoding.ASCII.GetBytes(_configuration["jwt:key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                            new Claim("Id",Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(ClaimTypes.Role, user.Role.RoleName)
                        }),
                    Expires = DateTime.Now.AddMinutes(30),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);
                return Ok(new { Token = stringToken, Role = user.Role.RoleName });
            }
            return Unauthorized();
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
        [Route("Register")]
        public async Task<IActionResult> RegisterAccount(AddAccountDTO dto)
        {
            var existingUser = _repository.GetUserByEmail(dto.Email);
            var user = _repository.CreateAccount(dto); 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString()),
                    new Claim(ClaimTypes.Email, dto.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            string confirmLink = $"http://localhost:5270/confirm-email?token={tokenString}";
            string body = $"<p>Nhấn vào link dưới đây để xác thực tài khoản:</p> <a href='{confirmLink}'>Xác thực email</a> <p>{dto.Password}</p>";
            await _emailConfig.SendEmailAsync(dto.Email, "Xác thực tài khoản", body);
            
            return Ok(new { Message = "Vui lòng kiểm tra email để xác thực tài khoản!" });
        }
    }
}
