using ManageHotel.Models;

namespace ManageHotel.DAO
{
    public class TypeRoomDAO
    {
        private readonly HotelManageContext _context;
        public TypeRoomDAO(HotelManageContext context)
        {
            _context=context;
        }

        public List<TypeRoom> GetAll()
        {
            return _context.TypeRooms.ToList();
        }
    }
}
