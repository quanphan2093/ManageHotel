using ManageHotel.DTOs.Bookings;

namespace ManageHotel.Repository
{
    public interface IBookingRepository
    {
        List<GetBookingDTO> GetAllBooking();
        GetBookingDTO CreateBooking (AddBookingDTO dto);
        void UpdateBooking (int id, UpdateBookingDTO dto);
        List<GetBookingDTO> GetBookingByPhoneNumber(string phonenumber);
        List<GetBookingDTO> GetBookingByDay(DateTime from, DateTime to);
    }
}
