using System.ComponentModel.DataAnnotations;

namespace ManageHotel.DTOs.PaymentTypes
{
    public class AddPaymentType
    {
        [Required]
        public string PaymentMethod { get; set; }
    }
}
