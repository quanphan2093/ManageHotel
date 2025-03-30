using ManageHotel.DTOs.Bookings;
using ManageHotel.DTOs.Rooms;

namespace ManageHotel.Repository
{
    public interface IRoomRepository
    {
        List<GetRoomDTO> GetAllRoom();
        void CreateRoom(AddRoomDTO room);
        void UpdateRoom(int id,UpdateRoomDTO room);
        void DeleteRoom(int roomId);
        List<GetRoomDTO> GetAllRoomUnBooking(DateTime from, DateTime to);
    }
}
