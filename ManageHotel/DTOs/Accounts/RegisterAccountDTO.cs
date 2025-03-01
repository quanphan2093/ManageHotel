namespace ManageHotel.DTOs.Accounts
{
    public class RegisterAccountDTO
    {
        public string Email { get; set; }
        public string Password { get; set; } 
        public string Name { get; set; } 
        public string PhoneNumber { get; set; } 
        public DateTime CreateAt { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
    }
}
