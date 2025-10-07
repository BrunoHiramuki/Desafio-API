using Desafio.Models;
using Desafio.Repositories;
using Desafio.Services;
using Moq;
using System;
using Xunit;

namespace Desafio.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _mockRepo;
        private readonly AccountService _service;

        public AccountServiceTests()
        {
            _mockRepo = new Mock<IAccountRepository>();
            _service = new AccountService(_mockRepo.Object);
        }

        [Fact]
        public void CreateAccount_NoLatePayment_ShouldNotApplyPenalty()
        {
            var account = new Account
            {
                Name = "Teste1",
                Value = 100,
                DueDate = new DateTime(2024, 01, 10),
                PaymentDate = new DateTime(2024, 01, 10) 
            };

            _service.CreateAccount(account);

            Assert.Equal(100, account.Value);
            Assert.Equal(0, account.DaysLate);
        }

        [Fact]
        public void CreateAccount_3DaysLate_ShouldApplyCorrectPenalty()
        {
            var account = new Account
            {
                Name = "Teste2",
                Value = 100,
                DueDate = new DateTime(2024, 01, 10),
                PaymentDate = new DateTime(2024, 01, 13)
            };

            _mockRepo.Setup(repo => repo.GetPenalties(3))
                    .Returns(new Penaltie { FinePercent = 2, DailyInterest = 0.1m });

            _service.CreateAccount(account);

            Assert.Equal(102.3m, account.Value);
            Assert.Equal(3, account.DaysLate);
        }

        [Fact]
        public void CreateAccount_PaymentBeforeDueDate_ShouldNotApplyPenalty()
        {
            var account = new Account
            {
                Name = "Teste3",
                Value = 100,
                DueDate = new DateTime(2024, 01, 10),
                PaymentDate = new DateTime(2024, 01, 05)
            };

            _service.CreateAccount(account);

            Assert.Equal(100, account.Value);
            Assert.Equal(0, account.DaysLate);
        }
    }
}