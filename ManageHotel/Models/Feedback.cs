﻿using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime CreateAt { get; set; }
        public int Voted { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public int? BlogId { get; set; }
        public int? HotelId { get; set; }

        public virtual Blog? Blog { get; set; }
        public virtual Hotel? Hotel { get; set; }
    }
}
