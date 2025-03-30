namespace ManageHotel.DTOs.Accounts
{
    public class UpdateAccountDTO
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; } 
        public DateTime UpdateAt { get; set; }
        public int RoleId { get; set; }
    }
}
