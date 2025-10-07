using Desafio.Models;
using System.Collections.Generic;

namespace Desafio.Repositories
{
    public interface IAccountRepository
    {
        public List<Account> GetAllAccount();
        public void AddAccount(Account account);
        public Penaltie GetPenalties(int days);
    }
}
