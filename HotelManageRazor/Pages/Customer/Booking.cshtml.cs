using ManageHotel.DAO;
using ManageHotel.DTOs.BookingDetails;
using ManageHotel.DTOs.Bookings;
using ManageHotel.DTOs.Rooms;
using ManageHotel.DTOs.TypeRooms;
using ManageHotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HotelManageRazor.Pages.Customer
{
    public class BookingModel : PageModel
    {
        private readonly HttpClient client;
        public string RoomApiUrl { get; set; } = "https://localhost:7036/api/Room";
        private readonly string BookingApiUrl;
        private readonly string BookingDetailApiUrl;
        private readonly string TypeRoomApiUrl;

        public BookingModel(IConfiguration configuration)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            RoomApiUrl = "https://localhost:7036/api/Room";
            TypeRoomApiUrl = "https://localhost:7036/api/TypeRoom";
            BookingApiUrl = "https://localhost:7036/api/Booking";
            BookingDetailApiUrl = "https://localhost:7036/api/BookingDetail";
        }

        public List<GetRoomDTO> GetRooms { get; set; } = new List<GetRoomDTO>();
        public List<GetTypeRoomDTO> GetTypeRooms { get; set; } = new List<GetTypeRoomDTO>();
        public List<int> GetRoomFloors { get; set; } = new List<int>();
        public string Error { get; set; }
        public async Task OnGet()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var roomResponse = await client.GetStringAsync(RoomApiUrl);
            var typeRoomResponse = await client.GetStringAsync(TypeRoomApiUrl);

            if (!string.IsNullOrEmpty(roomResponse) && !string.IsNullOrEmpty(typeRoomResponse))
            {
                GetRooms = JsonSerializer.Deserialize<List<GetRoomDTO>>(roomResponse, options) ?? new List<GetRoomDTO>();
                GetTypeRooms = JsonSerializer.Deserialize<List<GetTypeRoomDTO>>(typeRoomResponse, options) ?? new List<GetTypeRoomDTO>();

                GetRoomFloors = GetRooms.Select(x => x.RoomFloor).Distinct().ToList();
            }
        }

        public async Task OnPost(string name, string email, string phone, string start, string end, List<string> room)
        {
            foreach (var roomId in room)
            {
                Console.WriteLine("Room ID: " + roomId);
            }
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var bookingDto = new AddBookingDTO
                {
                    StartDate = DateTime.Parse(start),
                    EndDate = DateTime.Parse(end),
                    Email = email,
                    FullName = name,
                    PhoneNumber = phone,
                    Status = "Booking",
                    PaymentTypeId = 1
                };

                var jsonContent = new StringContent(JsonSerializer.Serialize(bookingDto, options), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(BookingApiUrl, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var createdBooking = JsonSerializer.Deserialize<GetBookingDTO>(responseContent, options);
                    if (createdBooking != null)
                    {
                        int newBookingId = createdBooking.BookingId;
                        List<AddBookingDetailDTO> bookingDetails = new List<AddBookingDetailDTO>();
                        foreach (var roomStr in room)
                        {
                            if (int.TryParse(roomStr, out int roomId))
                            {
                                AddBookingDetailDTO bookingDetailDTO = new AddBookingDetailDTO();
                                bookingDetailDTO.BookingId = newBookingId;
                                bookingDetailDTO.RoomId = roomId;
                                bookingDetailDTO.Quantity = 1;
                                bookingDetails.Add(bookingDetailDTO);
                            }
                        }
                        var detailJsonContent = new StringContent(JsonSerializer.Serialize(bookingDetails, options), Encoding.UTF8, "application/json");
                        var detailResponse = await client.PostAsync(BookingDetailApiUrl, detailJsonContent);

                        if (!detailResponse.IsSuccessStatusCode)
                        {
                            Error = "Booking details failed to save.";
                        }
                    }
                }
                else
                {
                    Error = "Booking failed.";
                }
            }
            catch (Exception ex)
            {
                Error = $"An error occurred: {ex.Message}";
            }
        }


    }
}
