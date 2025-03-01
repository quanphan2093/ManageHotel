using AutoMapper;
using ManageHotel.DAO;
using ManageHotel.DTOs.Accounts;
using ManageHotel.DTOs.Roles;
using ManageHotel.Models;

namespace ManageHotel.Repository.impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _dao;
        private readonly IMapper _mapper;
        public AccountRepository(AccountDAO dao, IMapper mapper)
        {
            _dao = dao;
            _mapper = mapper;
        }
        public void CreateAccount(AddAccountDTO account)
        {
            var a = _mapper.Map<Account>(account);
            _dao.CreateAccount(a);  
        }

        public void DeleteAccount(int id)
        {
            _dao.DeleteAccount(id);
        }

        public AccountRequestDTO GetAccountById(int id)
        {
            return _mapper.Map<AccountRequestDTO>(_dao.GetAccountById(id));
        }

        public List<AccountRequestDTO> GetAllAccount()
        {
            var a = _dao.GetAllAcount();
            var account = _mapper.Map<List<AccountRequestDTO>>(a);
            for(int i = 0; i<a.Count; i++)
            {
                account[i].Role = _mapper.Map<GetRoleDTO>(a[i].Role);
            }
            return account;
        }

        public AccountRequestDTO Login(string username, string password)
        {
            var a = _dao.Login(username,password);
            return _mapper.Map<AccountRequestDTO>(a);
        }

        public void UpdateAccount(int id, UpdateAccountDTO account)
        {
            var a = _mapper.Map<Account>(account);
            _dao.UpdateAccount(id, a);
        }
    }
}
