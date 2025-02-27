using ManageHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace ManageHotel.DTOs.Hotels
{
    public class AddHotelDTO
    {
        [Required]
        public string HotelName { get; set; }
        [Required] 
        public string? Address { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? Rating { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public int AccountId { get; set; }
    }
}
