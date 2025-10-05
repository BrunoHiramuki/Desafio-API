using Desafio.Context;
using Desafio.Models;
using Desafio.Repositories;
using System;
using System.Collections.Generic;

namespace Desafio.Services
{
    public class AccountService
    {
        private readonly AccountRepository _repository;

        public AccountService(AccountRepository repository)
        {
            _repository = repository;
        }

        public List<Account> GetAccount()
        {
            return _repository.GetAllAccount(); ;
        }

        public void CreateAccount(Account account)
        {
            if (account.PaymentDate > DateTime.UtcNow)
            {
                throw new Exception("Data de pagamento não pode ser maior que hoje!");
            }

            TimeSpan days = account.PaymentDate - account.DueDate;

            var penaltie = _repository.GetPenalties(days.Days);

            account.Value = account.Value + 
                (account.Value * (penaltie.FinePercent/100)) + 
                (account.Value * (days.Days * (penaltie.DailyInterest / 100)));

            account.DaysLate = days.Days;

            _repository.AddAccount(account);
        }
    }
}
