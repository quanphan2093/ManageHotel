using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.Roles;

namespace ManageHotel.Repository.impl
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IMapper _mapper;
        private readonly RoleDAO _dao;
        public RoleRepository(IMapper mapper, RoleDAO dao)
        {
            _mapper = mapper;
            _dao = dao;
        }
        public List<GetRoleDTO> GetAllRoles()
        {   
            return _mapper.Map<List<GetRoleDTO>>(_dao.GetRoles());
        }
    }
}
