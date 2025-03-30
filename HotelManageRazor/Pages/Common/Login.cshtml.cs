using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HotelManageRazor.Pages.Common
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string AccountApiUrl;
        private readonly string BookingDetailApiUrl;
        private readonly string TypeRoomApiUrl;
        public string Message { get;set; }
        public LoginModel(IConfiguration configuration)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            TypeRoomApiUrl = "https://localhost:7036/api/TypeRoom";
            AccountApiUrl = "https://localhost:7036/api/Account";
            BookingDetailApiUrl = "https://localhost:7036/api/BookingDetail";
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Message = "Email or password cannot be empty";
                return Page();
            }

            string loginUrl = $"{AccountApiUrl}/Login";

            var loginData = new { Email = email, Password = password };
            var jsonContent = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(loginUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var token = JsonSerializer.Deserialize<string>(responseData);

                    HttpContext.Session.SetString("AuthToken", token);

                    return RedirectToPage("/Home");
                }
                else
                {
                    Message = "Invalid email or password";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Message = "An error occurred while logging in. Please try again.";
                return Page();
            }
        }
    }
}
