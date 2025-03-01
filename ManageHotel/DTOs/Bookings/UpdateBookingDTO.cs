using ManageHotel.Models;

namespace ManageHotel.DTOs.Bookings
{
    public class UpdateBookingDTO
    {
        public string FullName { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
    }
}
