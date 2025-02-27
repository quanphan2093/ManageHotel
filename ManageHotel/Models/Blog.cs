using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? BriefInfo { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int HotelId { get; set; }
        public int? FeedbackId { get; set; }

        public virtual Feedback? Feedback { get; set; }
        public virtual Hotel Hotel { get; set; } = null!;
    }
}
