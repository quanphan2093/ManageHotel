using ManageHotel.DTOs.Accounts;
using ManageHotel.DTOs.Rooms;
using ManageHotel.DTOs.TypeRooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HotelManageRazor.Pages.Manage
{
    public class RoomMangeModel : PageModel
    {
        private readonly HttpClient client;
        public string RoomApiUrl { get; set; } = "https://localhost:7036/api/Room";
        public RoomMangeModel(IConfiguration configuration)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            RoomApiUrl = "https://localhost:7036/api/Room";
        }
        public List<GetRoomDTO> GetRooms { get; set; } = new List<GetRoomDTO>();
        public async Task<IActionResult> OnGet() {
            var token = HttpContext.Session.GetString("JWT");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Common/403");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var roomResponse = await client.GetStringAsync(RoomApiUrl);

            if (!string.IsNullOrEmpty(roomResponse))
            {
                GetRooms = JsonSerializer.Deserialize<List<GetRoomDTO>>(roomResponse, options) ?? new List<GetRoomDTO>();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostActive(int id)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var deleteAccount = RoomApiUrl + "/" + id;
            var response = await client.DeleteAsync(deleteAccount);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to activate account.");
            }
        }
    }
}
