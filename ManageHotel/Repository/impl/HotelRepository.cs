using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.Accounts;
using ManageHotel.DTOs.Hotels;
using ManageHotel.Models;

namespace ManageHotel.Repository.impl
{
    public class HotelRepository : IHotelRepository
    {
        private readonly IMapper _mapper;
        private readonly HotelDAO _dao;
        public HotelRepository(IMapper mapper, HotelDAO dao)
        {
            _mapper= mapper;
            _dao= dao;
        }
        public void CreateHotel(AddHotelDTO hotel)
        {
            var h = _mapper.Map<Hotel>(hotel);
            _dao.CreateHotel(h);
        }

        public void DeleteHotel(int id)
        {
            _dao.DeleteHotel(id);
        }

        public List<GetHotelDTO> GetAllHotel()
        {
            var h = _dao.GetAllHotel();
            var hotel = _mapper.Map<List<GetHotelDTO>>(h);
            for(int i = 0; i< h.Count; i++)
            {
                hotel[i].Account = _mapper.Map<AccountRequestDTO>(h[i].Account);
            }
            return hotel;
        }

        public GetHotelDTO GetHotelById(int id)
        {
            var h = _dao.GetHotelById(id);
            return _mapper.Map<GetHotelDTO>(h);
        }

        public void UpdateHotel(int id, UpdateHotelDTO hotel)
        {
            var h = _mapper.Map<Hotel>(hotel);
            _dao.UpdateHotel(id, h);
        }
    }
}
