using ManageHotel.DTOs.TypeRooms;

namespace ManageHotel.Repository
{
    public interface ITypeRoomRepository
    {
        List<GetTypeRoomDTO> GetAll();
    }
}
