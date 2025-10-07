using Desafio.Models;
using Desafio.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Desafio.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("read")]
        public ActionResult<List<Account>> Read()
        {
            return Ok(_service.GetAccount());
        }

        [HttpPost("create")]
        public ActionResult Create(Account account)
        {
            try
            {
                _service.CreateAccount(account);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
