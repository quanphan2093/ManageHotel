﻿using ManageHotel.DTOs.Blogs;
using ManageHotel.DTOs.Hotels;
using ManageHotel.Models;

namespace ManageHotel.DTOs.Feedbacks
{
    public class GetFeedbackDTO
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

        public GetBlogDTO Blog { get; set; }
        public GetHotelDTO Hotel { get; set; }
    }
}
