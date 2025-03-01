using ManageHotel.DTOs.Accounts;
using ManageHotel.Models;

namespace ManageHotel.DTOs.Hotels
{
    public class GetHotelDTO
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Rating { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int AccountId { get; set; }
        public AccountRequestDTO Account { get; set; }
    }
}
