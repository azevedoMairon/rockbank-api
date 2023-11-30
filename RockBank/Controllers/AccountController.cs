using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public IResult Create(AccountDTO accountDTO, ApplicationDBContext context)
        {
            Customer customer = context.Customers.Find(accountDTO.CustomerId);

            if (customer == null)
                return Results.BadRequest("There's no such Customer with the given Id");

            Account account = new Account
            {
                Number = accountDTO.Number,
                CustomerId = accountDTO.CustomerId,
                Balance = accountDTO.Balance
            };

            context.Accounts.Add(account);
            context.SaveChanges();

            return Results.Created($"account/{account.Id}", account);
        }

        [HttpGet]
        public IResult Read(ApplicationDBContext context)
        {
            List<Account> accounts = context.Accounts.ToList();
            return Results.Ok(accounts);
        }
    }
}

