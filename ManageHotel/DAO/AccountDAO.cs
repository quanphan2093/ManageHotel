﻿using AutoMapper;
using ManageHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageHotel.DAO
{
    public class AccountDAO
    {
        private readonly HotelManageContext _context;
        public AccountDAO(HotelManageContext context)
        {
            _context = context;            
        }

        public List<Account> GetAllAcount()
        {
            return _context.Accounts.Include(x => x.Role).ToList();
        }

        public Account GetAccountById(int id)
        {
            return _context.Accounts.Include(x => x.Role).SingleOrDefault(x => x.AccountId == id);
        }

        public void CreateAccount(Account account)
        {
            if (account.CreateAt < new DateTime(1753, 1, 1) || account.CreateAt > new DateTime(9999, 12, 31))
            {
                account.CreateAt = DateTime.UtcNow;
            }
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }
        public void UpdateAccount(int id, Account account)
        {
            var a = _context.Accounts.Find(id);
            if(a != null)
            {
                a.PhoneNumber=account.PhoneNumber;
                a.Name = account.Name;
                a.UpdateAt=DateTime.UtcNow;
                a.IsDeleted = false;
                _context.Accounts.Update(a);
                _context.SaveChanges();
            }
        }
        public void DeleteAccount(int id)
        {
            var a =_context.Accounts.Find(id);
            if(a != null)
            {
                a.IsDeleted = !a.IsDeleted;
                _context.Accounts.Update(a);
                _context.SaveChanges(); 
            }
        }

        public Account Login(string email, string password)
        {
            return _context.Accounts.Include(x => x.Role).Where(x => x.Email.Equals(email) && x.Password.Equals(password)).FirstOrDefault();
        }

        public Account GetUserByEmail(string email)
        {
            return _context.Accounts.Include(x => x.Role).Where(x => x.Email.Equals(email)).FirstOrDefault();
        }

    }
}
