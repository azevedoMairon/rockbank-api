using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WithdrawController : ControllerBase
    {
        [HttpPost]
        public IResult Withdraw(WithdrawDTO withdrawDTO, ApplicationDBContext context)
        {
            Account account = context.Accounts.Find(withdrawDTO.Account);

            if (account == null)
                return Results.BadRequest("There's no such Account with the given Id");

            if(account.Balance - withdrawDTO.Value < 0)
            {
                return Results.BadRequest("There's no suficient balance to accomplish this withdraw"); ;
            }

            Customer customer = context.Customers.Find(account.CustomerId);

            Withdraw withdraw = new Withdraw
            {
                Value = withdrawDTO.Value,
                SourceId = account.Id,
                CreatedBy = customer.Name,
            };

            account.Balance = account.Balance - withdrawDTO.Value;
            account.Transactions.Add(withdraw);

            account.EditInfo(account.Balance, account.Transactions);

            context.Transactions.Add(withdraw);

            context.SaveChanges();

            return Results.Ok(withdraw);
        }
    }
}
