using System.ComponentModel.DataAnnotations;

namespace ManageHotel.DTOs.PaymentTypes
{
    public class UpdatePaymentTypeDTO
    {
        [Required]
        public string PaymentMethod { get; set; }
    }
}
