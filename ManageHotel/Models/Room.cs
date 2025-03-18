using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class Room
    {
        public Room()
        {
            BookingDetails = new HashSet<BookingDetail>();
            RoomImages = new HashSet<RoomImage>();
        }

        public int RoomId { get; set; }
        public int RoomFloor { get; set; }
        public int RoomNumber { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public int HotelId { get; set; }
        public int TypeRoomId { get; set; }

        public virtual Hotel Hotel { get; set; } = null!;
        public virtual TypeRoom TypeRoom { get; set; } = null!;
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
        public virtual ICollection<RoomImage> RoomImages { get; set; }
    }
}
