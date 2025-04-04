﻿using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? BriefInfo { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
