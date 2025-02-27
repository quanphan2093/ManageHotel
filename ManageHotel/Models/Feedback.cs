using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            Blogs = new HashSet<Blog>();
            Hotels = new HashSet<Hotel>();
        }

        public int FeedbackId { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime CreateAt { get; set; }
        public int Voted { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
