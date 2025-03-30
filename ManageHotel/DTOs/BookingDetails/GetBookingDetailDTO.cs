using ManageHotel.DTOs.Rooms;
using ManageHotel.Models;

namespace ManageHotel.DTOs.BookingDetails
{
    public class GetBookingDetailDTO
    {
        public int BookingDetailsId { get; set; }
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        //public virtual Booking Booking { get; set; } = null!;
        public virtual GetRoomDTO Room { get; set; } = null!;
    }
}
