using ManageHotel.DTOs.Accounts;

namespace ManageHotel.Repository
{
    public interface IAccountRepository
    {
        List<AccountRequestDTO> GetAllAccount();
        AccountRequestDTO GetAccountById(int id);
        AccountRequestDTO CreateAccount(AddAccountDTO account);
        void UpdateAccount(int id, UpdateAccountDTO account);
        void DeleteAccount(int id);
        AccountRequestDTO Login(string username, string password);
        AccountRequestDTO GetUserByEmail(string email); 
    }
}
