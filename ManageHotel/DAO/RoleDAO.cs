using ManageHotel.Models;

namespace ManageHotel.DAO
{
    public class RoleDAO
    {
        private readonly HotelManageContext _context;
        public RoleDAO(HotelManageContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
