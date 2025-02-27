using ManageHotel.Models;

namespace ManageHotel.DTOs.Rooms
{
    public class AddRoomDTO
    {
        public int RoomFloor { get; set; }
        public int RoomNumber { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public int HotelId { get; set; }
        public int TypeRoomId { get; set; }
    }
}
