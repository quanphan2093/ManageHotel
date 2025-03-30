using ManageHotel.DTOs.Roles;

namespace ManageHotel.Repository
{
    public interface IRoleRepository
    {
        List<GetRoleDTO> GetAllRoles();
    }
}
