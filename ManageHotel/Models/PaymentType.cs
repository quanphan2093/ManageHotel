using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Bookings = new HashSet<Booking>();
        }

        public int PaymentTypeId { get; set; }
        public string PaymentMethod { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
