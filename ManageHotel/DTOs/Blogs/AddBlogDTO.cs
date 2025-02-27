using ManageHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace ManageHotel.DTOs.Blogs
{
    public class AddBlogDTO
    {
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? BriefInfo { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int HotelId { get; set; }
    }
}
