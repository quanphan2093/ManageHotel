using ManageHotel.Models;

namespace ManageHotel.DTOs.Blogs
{
    public class UpdateBlogDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? BriefInfo { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int HotelId { get; set; }
    }
}
