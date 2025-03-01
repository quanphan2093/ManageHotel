using ManageHotel.DTOs.Roles;
using ManageHotel.Models;

namespace ManageHotel.DTOs.Accounts
{
    public class AccountRequestDTO
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; } 
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public GetRoleDTO Role { get; set; }
    }
}
