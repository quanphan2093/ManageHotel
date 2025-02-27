using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            Blogs = new HashSet<Blog>();
            Feedbacks = new HashSet<Feedback>();
            Rooms = new HashSet<Room>();
        }

        public int HotelId { get; set; }
        public string HotelName { get; set; } = null!;
        public string? Address { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public string? Rating { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
