using Desafio.Context;
using Desafio.Models;
using Desafio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace Desafio.Tests.Repositories
{
    public class AccountRepositoryTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly AccountRepository _repository;

        public AccountRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _repository = new AccountRepository(_context);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            _context.Penalties.AddRange(
                new Penaltie { DaysLateFrom = 1, DaysLateTo = 4, FinePercent = 2, DailyInterest = 0.1m },
                new Penaltie { DaysLateFrom = 4, DaysLateTo = 6, FinePercent = 3, DailyInterest = 0.2m },
                new Penaltie { DaysLateFrom = 6, DaysLateTo = null, FinePercent = 5, DailyInterest = 0.3m }
            );
            _context.SaveChanges();
        }

        [Fact]
        public void GetPenalties_3DaysLate_ShouldReturnFirstRule()
        {
            var result = _repository.GetPenalties(3);

            Assert.NotNull(result);
            Assert.Equal(2, result.FinePercent);
            Assert.Equal(0.1m, result.DailyInterest);
        }

        [Fact]
        public void GetPenalties_5DaysLate_ShouldReturnSecondRule()
        {
            var result = _repository.GetPenalties(5);

            Assert.NotNull(result);
            Assert.Equal(3, result.FinePercent);
            Assert.Equal(0.2m, result.DailyInterest);
        }

        [Fact]
        public void GetPenalties_10DaysLate_ShouldReturnThirdRule()
        {
            var result = _repository.GetPenalties(10);

            Assert.NotNull(result);
            Assert.Equal(5, result.FinePercent);
            Assert.Equal(0.3m, result.DailyInterest);
        }

        [Fact]
        public void GetAllAccount_ShouldReturnAllAccounts()
        {
            _context.Accounts.Add(new Account {
                Name = "Teste1",
                Value = 100,
                DueDate = new DateTime(2024, 01, 10),
                PaymentDate = new DateTime(2024, 01, 05)
            });
            _context.Accounts.Add(new Account
            {
                Name = "Teste2",
                Value = 200,
                DueDate = new DateTime(2024, 01, 10),
                PaymentDate = new DateTime(2024, 01, 10)
            });
            _context.SaveChanges();

            var result = _repository.GetAllAccount();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void AddAccount_ShouldIncreaseCount()
        {
            var initialCount = _context.Accounts.Count();
            var account = new Account {
                Name = "Teste3",
                Value = 300,
                DueDate = new DateTime(2024, 01, 10),
                PaymentDate = new DateTime(2024, 01, 05)
            };

            _repository.AddAccount(account);

            Assert.Equal(initialCount + 1, _context.Accounts.Count());
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}