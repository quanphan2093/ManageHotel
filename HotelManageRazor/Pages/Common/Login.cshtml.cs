using ManageHotel.DTOs.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static QRCoder.PayloadGenerator;

namespace HotelManageRazor.Pages.Common
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string AccountApiUrl;
        private readonly string LoginApiUrl;
        public string Message { get;set; }
        public LoginModel(IConfiguration configuration)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            AccountApiUrl = "https://localhost:7036/api/Account";
            LoginApiUrl = "https://localhost:7036/api/Account/Login";
        }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Email và mật khẩu không được để trống!";
                return Page();
            }

            var loginData = new { email = email.Trim(), password = password.Trim() };
            Console.WriteLine(loginData);
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.GetAsync($"https://localhost:7036/api/Account/Login?email={email}&password={password}");
                Console.WriteLine(response);
                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = "Đăng nhập thất bại! Kiểm tra lại email hoặc mật khẩu.";
                    return Page();
                }

                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AccountResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result != null)
                {
                    HttpContext.Session.Clear();  
                    HttpContext.Session.SetString("JWT", result.Token);
                    HttpContext.Session.SetString("UserRole", result.Role);
                    return LocalRedirect("/Dashboard");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                ErrorMessage = "Có lỗi xảy ra. Vui lòng thử lại sau.";
            }

            return Page();
        }
    }
}
