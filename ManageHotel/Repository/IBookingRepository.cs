using ManageHotel.DTOs.Bookings;

namespace ManageHotel.Repository
{
    public interface IBookingRepository
    {
        List<GetBookingDTO> GetAllBooking();
        GetBookingDTO CreateBooking (AddBookingDTO dto);
        void UpdateBooking (int id, UpdateBookingDTO dto);
    }
}
