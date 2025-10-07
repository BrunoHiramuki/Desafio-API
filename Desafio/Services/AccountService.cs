using Desafio.Models;
using Desafio.Repositories;
using System;
using System.Collections.Generic;

namespace Desafio.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public List<Account> GetAccount()
        {
            return _repository.GetAllAccount(); ;
        }

        public void CreateAccount(Account account)
        {
            TimeSpan days = account.PaymentDate - account.DueDate;
            int daysLate = days.Days > 0 ? days.Days : 0;

            if(daysLate == 0)
            {
                account.DaysLate = 0;
                _repository.AddAccount(account);
                return;
            }

            var penaltie = _repository.GetPenalties(days.Days);

            account.Value = account.Value +
                (account.Value * (penaltie.FinePercent / 100)) +
                (account.Value * (days.Days * (penaltie.DailyInterest / 100)));

            account.DaysLate = days.Days;

            _repository.AddAccount(account);
        }
    }
}
