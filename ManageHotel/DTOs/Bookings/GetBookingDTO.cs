using ManageHotel.Models;

namespace ManageHotel.DTOs.Bookings
{
    public class GetBookingDTO
    {
        public int BookingId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public int PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; };
    }
}
