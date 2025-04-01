using ManageHotel.DTOs.Bookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace HotelManageRazor.Pages.Customer
{
    public class MyBookingModel : PageModel
    {
        private readonly HttpClient client;
        private readonly string BookingApiUrl;

        public MyBookingModel(IConfiguration configuration)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BookingApiUrl = "https://localhost:7036/api/Booking";
        }

        public List<GetBookingDTO> GetBookingDTOs { get; set; } = new();

        public async Task<IActionResult> OnGetSearch(string phonenumber)
        {
            if (!string.IsNullOrEmpty(phonenumber))
            {
                try
                {
                    var response = await client.GetStringAsync($"{BookingApiUrl}/{phonenumber}");
                    GetBookingDTOs = JsonSerializer.Deserialize<List<GetBookingDTO>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();
                    GetBookingDTOs = GetBookingDTOs.OrderByDescending(x => x.BookingId).ToList();
                }
                catch
                {
                    GetBookingDTOs = new();
                }
            }

            return Content(RenderBookingTable(), "text/html");
        }

        private string RenderBookingTable()
        {
            if (GetBookingDTOs == null || !GetBookingDTOs.Any())
            {
                return "<p class='text-danger'>Không tìm thấy kết quả.</p>";
            }

            var tableHtml = "<table class='table table-striped'>" +
                            "<thead><tr><td>FullName</td><td>Email</td><td>StartDate</td><td>EndDate</td><td>Status</td><td>PaymentType</td><td>Room</td></tr></thead>" +
                            "<tbody>";

            foreach (var i in GetBookingDTOs)
            {
                tableHtml += "<tr>" +
                             $"<td>{i.FullName}</td>" +
                             $"<td>{i.Email}</td>" +
                             $"<td>{i.StartDate:dd/MM/yyyy}</td>" +
                             $"<td>{i.EndDate:dd/MM/yyyy}</td>" +
                             $"<td>{i.Status}</td>" +
                             $"<td>{i.PaymentType.PaymentMethod}</td>" +
                             "<td>";

                foreach (var r in i.BookingDetails)
                {
                    tableHtml += $"<p>{r.Room.RoomNumber}</p>";
                }

                tableHtml += "</td></tr>";
            }

            tableHtml += "</tbody></table>";
            return tableHtml;
        }
    }
}
