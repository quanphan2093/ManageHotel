using System.ComponentModel.DataAnnotations;

namespace ManageHotel.DTOs.Accounts
{
    public class AddAccountDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
