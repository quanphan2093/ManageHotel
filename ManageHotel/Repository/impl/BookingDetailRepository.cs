using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.BookingDetails;
using ManageHotel.Models;

namespace ManageHotel.Repository.impl
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly BookingDetailsDAO _dao;
        private readonly IMapper _mapper;
        public BookingDetailRepository(BookingDetailsDAO dao, IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }
        public void CreateBookingDetail( List<AddBookingDetailDTO> dto)
        {
            var booking = _mapper.Map<List<BookingDetail>>(dto);
            _dao.CreateBookingDetail(booking);
        }
    }
}
