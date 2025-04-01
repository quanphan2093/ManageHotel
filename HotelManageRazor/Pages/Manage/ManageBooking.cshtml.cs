using ManageHotel.DTOs.Bookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using static QRCoder.PayloadGenerator;
using System.Text.Json;

namespace HotelManageRazor.Pages.Manage
{
    public class ManageBookingModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string BookingApiUrl;
        public ManageBookingModel(IConfiguration configuration)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BookingApiUrl = "https://localhost:7036/api/Booking";
        }
        public List<GetBookingDTO> GetBookingDTOs { get; set; } = new();
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Common/403");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetStringAsync($"{BookingApiUrl}");
            GetBookingDTOs = JsonSerializer.Deserialize<List<GetBookingDTO>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
            GetBookingDTOs = GetBookingDTOs.OrderByDescending(x => x.BookingId).ToList();
            return Page();
        }

    }
}
