using ManageHotel.Models;

namespace ManageHotel.DTOs.RoomImages
{
    public class GetRoomImageDTO
    {
        public int RoomImagesId { get; set; }
        public string ImageName { get; set; } = null!;
        public int RoomId { get; set; }
        public virtual Room Room { get; set; } = null!;
    }
}
