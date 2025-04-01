using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManageRazor.Pages.Common
{
    public class ComfirmEmailModel : PageModel
    {
        private readonly IConfiguration _config;

        public ComfirmEmailModel(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult OnGet(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                var email = principal.FindFirstValue(ClaimTypes.Email);

                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest("Token không hợp lệ hoặc đã hết hạn.");
                }
                return RedirectToPage("/Manage/Profile", new { id = userId });
            }
            catch (Exception)
            {
                return BadRequest("Lỗi xác thực email.");
            }
        }
    }
}
