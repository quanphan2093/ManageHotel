using ManageHotel.DTOs.Feedbacks;
using ManageHotel.DTOs.Hotels;
using ManageHotel.Models;

namespace ManageHotel.DTOs.Blogs
{
    public class GetBlogDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; } 
        public string? Description { get; set; }
        public string? BriefInfo { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int HotelId { get; set; }
        public GetHotelDTO Hotel { get; set; } 
    }
}
