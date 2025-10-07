using Desafio.Context;
using Desafio.Models;
using Desafio.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Desafio.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Account> GetAllAccount()
        {
            return _context.Accounts.ToList();
        }

        public void AddAccount(Account account) 
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public Penaltie GetPenalties(int days)
        {
            return _context.Penalties.FirstOrDefault(p => days >= p.DaysLateFrom && (p.DaysLateTo == null || days < p.DaysLateTo));
        }
    }
}
