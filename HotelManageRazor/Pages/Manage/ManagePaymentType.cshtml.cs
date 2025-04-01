using ManageHotel.DTOs.Bookings;
using ManageHotel.DTOs.PaymentTypes;
using ManageHotel.DTOs.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HotelManageRazor.Pages.Manage
{
    public class ManagePaymentTypeModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string PaymentUrl;

        public ManagePaymentTypeModel(IConfiguration configuration)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            PaymentUrl = "https://localhost:7036/api/Payment";
        }
        public List<GetPaymentTypeDTO> GetPaymentTypeDTOs { get; set; } = new();
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Common/403");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = await client.GetStringAsync(PaymentUrl);

            if (!string.IsNullOrEmpty(response))
            {
                GetPaymentTypeDTOs = JsonSerializer.Deserialize<List<GetPaymentTypeDTO>>(response, options) ?? new List<GetPaymentTypeDTO>();
            }
            return Page();
        }
    }
}
