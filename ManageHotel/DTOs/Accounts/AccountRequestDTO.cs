using ManageHotel.Models;

namespace ManageHotel.DTOs.Accounts
{
    public class AccountRequestDTO
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}
