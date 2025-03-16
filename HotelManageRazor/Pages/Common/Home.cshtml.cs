using ManageHotel.DTOs.Hotels;
using ManageHotel.DTOs.Rooms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HotelManageRazor.Common
{
    public class HomeModel : PageModel
    {
        private readonly HttpClient client = null;
        private string HotelApiUrl = "";
        private string RoomApiUrl = "";
        private readonly IConfiguration configuration;
        public HomeModel(IConfiguration _configuration)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HotelApiUrl = "https://localhost:5185/api/Hotel";
            RoomApiUrl = "https://localhost:7036/api/Room";
            configuration = _configuration;
        }
        public List<GetRoomDTO> GetRooms { get; set; }
        public async Task OnGet()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var roomResponse = await client.GetStringAsync(RoomApiUrl);
            if (!string.IsNullOrEmpty(roomResponse))
            {
                GetRooms = JsonSerializer.Deserialize<List<GetRoomDTO>>(roomResponse, options) ?? new List<GetRoomDTO>();
                GetRooms = GetRooms.OrderByDescending(r => r.RoomId).Take(3).ToList();
            }
        }
    }
}
