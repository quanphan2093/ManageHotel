using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class BookingDetail
    {
        public int BookingDetailsId { get; set; }
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
