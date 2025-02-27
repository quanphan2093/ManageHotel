using ManageHotel.Models;

namespace ManageHotel.DTOs.BookingDetails
{
    public class AddBookingDetailDTO
    {   
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
