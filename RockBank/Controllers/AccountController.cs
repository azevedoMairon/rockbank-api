using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;
using RockBank.Services;
using RockBank.Utils;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly CustomerService _customerService;

        public AccountController(AccountService accountService, CustomerService customerService)
        {
            _accountService = accountService;
            _customerService = customerService;
        }

        [HttpPost]
        public IResult Create(AccountDTO accountDTO)
        {
            if (accountDTO == null)
                return Results.BadRequest();

            if(!accountDTO.IsValid)
                return Results.ValidationProblem(accountDTO.Notifications.ConvertToProblemDetails());

            Customer customer = _customerService.Get(accountDTO.CustomerId);

            if (customer != null)
            {
                Account account = _accountService.Create(accountDTO);
                return Results.Created($"account/{account.Id}", account);
            }
            else
            {
                return Results.NotFound("There's no such Customer with the given Id");
            }
        }

        [HttpGet]
        public IResult Read()
        {
            List<Account> accounts = _accountService.GetAll();
            return Results.Ok(accounts);
        }
    }
}

