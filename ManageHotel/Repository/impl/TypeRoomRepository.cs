using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.TypeRooms;

namespace ManageHotel.Repository.impl
{
    public class TypeRoomRepository : ITypeRoomRepository
    {
        private readonly TypeRoomDAO _dao;
        private readonly IMapper _mapper;
        public TypeRoomRepository(TypeRoomDAO dao, IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;   
        }
        public List<GetTypeRoomDTO> GetAll()
        {
            var type = _dao.GetAll();   
            return _mapper.Map<List<GetTypeRoomDTO>>(type);
        }
    }
}
