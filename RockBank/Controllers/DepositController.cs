using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepositController : ControllerBase
    {
        [HttpPost]
        public IResult Deposit(DepositDTO depositDTO, ApplicationDBContext context)
        {
            Account account = context.Accounts.Find(depositDTO.Account);
            Customer customer = context.Customers.Find(account.CustomerId);

            if (account == null)
                return Results.BadRequest("There's no such Account with the given Id");

            Deposit deposit = new Deposit(depositDTO.Value, account.Id, customer.Name);

            account.Balance = account.Balance + deposit.Value;
            account.Transactions.Add(deposit);
            account.EditInfo(account.Balance, account.Transactions);
            
            context.Transactions.Add(deposit);
            context.SaveChanges();

            return Results.Ok(new TransactionDTO(deposit.Type, deposit.Value, deposit.Tax, deposit.CreatedBy, deposit.CreatedOn, account.Balance));
        }
    }
}
