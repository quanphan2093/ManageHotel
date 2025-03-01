using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.Bookings;
using ManageHotel.DTOs.PaymentTypes;
using ManageHotel.Models;

namespace ManageHotel.Repository.impl
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IMapper _mapper;
        private readonly BookingDAO _dao;
        public BookingRepository(IMapper mapper, BookingDAO dao)
        {
            _mapper = mapper;
            _dao = dao;
        }
        public void CreateBooking(AddBookingDTO dto)
        {
            var booking = _mapper.Map<Booking>(dto);
            _dao.CreateBooking(booking);
        }

        public List<GetBookingDTO> GetAllBooking()
        {
            var b = _dao.GetAllBooking();
            var booking = _mapper.Map<List<GetBookingDTO>>(b);
            for (var i = 0; i < booking.Count; i++)
            {
                booking[i].PaymentType = _mapper.Map<GetPaymentTypeDTO>(b[i].PaymentType);
            }
            return booking;
        }

        public void UpdateBooking(int id, UpdateBookingDTO dto)
        {
            var booking = _mapper.Map<Booking>(dto);
            _dao.UpdateBooking(id, booking);
        }
    }
}
