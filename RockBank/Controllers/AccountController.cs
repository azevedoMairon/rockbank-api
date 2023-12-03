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
        private readonly ApplicationDBContext _context;

        public AccountController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IResult Create(AccountDTO accountDTO)
        {
            if (accountDTO == null)
                return Results.BadRequest();

            Customer customer = _context.Customers.Find(accountDTO.CustomerId);

            if (customer == null)
                return Results.NotFound("There's no such Customer with the given Id");

            Account account = new Account(accountDTO.Number, accountDTO.Balance, accountDTO.CustomerId);

            _context.Accounts.Add(account);
            _context.SaveChanges();

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

