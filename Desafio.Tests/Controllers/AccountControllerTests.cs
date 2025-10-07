using Desafio.Controllers;
using Desafio.Models;
using Desafio.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Desafio.Tests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _mockService;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockService = new Mock<IAccountService>();
            _controller = new AccountController(_mockService.Object);
        }

        [Fact]
        public void Read_ShouldReturnAllAccounts()
        {
            var accounts = new List<Account>
            {
                new Account {
                    Name = "Teste1",
                    Value = 100,
                    DueDate = new DateTime(2024, 01, 10),
                    PaymentDate = new DateTime(2024, 01, 05)
                },
                new Account {
                    Name = "Teste2",
                    Value = 100,
                    DueDate = new DateTime(2024, 01, 10),
                    PaymentDate = new DateTime(2024, 01, 10) 
                }
            };

            _mockService.Setup(service => service.GetAccount())
                       .Returns(accounts);

            var result = _controller.Read();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAccounts = Assert.IsType<List<Account>>(okResult.Value);
            Assert.Equal(2, returnedAccounts.Count);
        }

        [Fact]
        public void Create_ShouldCallService()
        {
            var account = new Account {
                Name = "Teste3",
                Value = 100,
                DueDate = new DateTime(2024, 01, 10),
                PaymentDate = new DateTime(2024, 01, 05)
            };

            var result = _controller.Create(account);

            _mockService.Verify(service => service.CreateAccount(account), Times.Once);
            Assert.IsType<OkResult>(result);
        }
    }
}