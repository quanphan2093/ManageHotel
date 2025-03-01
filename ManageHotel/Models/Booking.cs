using System;
using System.Collections.Generic;

namespace ManageHotel.Models
{
    public partial class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public int BookingId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } 
        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public int PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; } 
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
