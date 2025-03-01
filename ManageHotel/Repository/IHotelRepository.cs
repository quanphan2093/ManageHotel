using ManageHotel.DTOs.Hotels;

namespace ManageHotel.Repository
{
    public interface IHotelRepository
    {
        List<GetHotelDTO> GetAllHotel();
        GetHotelDTO GetHotelById(int id);
        void CreateHotel(AddHotelDTO hotel);
        void UpdateHotel(int id, UpdateHotelDTO hotel);
        void DeleteHotel(int id);   
    }
}
