using ManageHotel.DTOs.BookingDetails;

namespace ManageHotel.Repository
{
    public interface IBookingDetailRepository
    {
        void CreateBookingDetail( List<AddBookingDetailDTO> dto);
    }
}
