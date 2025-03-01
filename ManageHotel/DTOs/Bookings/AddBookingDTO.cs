using ManageHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace ManageHotel.DTOs.Bookings
{
    public class AddBookingDTO
    {
        [Required]
        public string FullName { get; set; } 
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }
    }
}
