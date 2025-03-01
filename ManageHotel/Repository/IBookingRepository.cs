using ManageHotel.DTOs.Bookings;

namespace ManageHotel.Repository
{
    public interface IBookingRepository
    {
        List<GetBookingDTO> GetAllBooking();
        void CreateBooking (AddBookingDTO dto);
        void UpdateBooking (int id, UpdateBookingDTO dto);
    }
}
