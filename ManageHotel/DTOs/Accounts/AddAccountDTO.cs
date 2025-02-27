using System.ComponentModel.DataAnnotations;

namespace ManageHotel.DTOs.Accounts
{
    public class AddAccountDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
