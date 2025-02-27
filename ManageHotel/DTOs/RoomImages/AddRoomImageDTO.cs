using ManageHotel.Models;

namespace ManageHotel.DTOs.RoomImages
{
    public class AddRoomImageDTO
    {

        public int RoomImagesId { get; set; }
        public string ImageName { get; set; }
        public int RoomId { get; set; }
    }
}
