using Desafio.Models;
using System.Collections.Generic;

namespace Desafio.Services
{
    public interface IAccountService
    {
        public List<Account> GetAccount();
        public void CreateAccount(Account account);
    }
}
