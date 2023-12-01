using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;
using RockBank.Utils;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController
    {
        [HttpPost]
        public IResult Transfer(TransferDTO transferDTO, ApplicationDBContext context)
        {
            Account sourceAccount = context.Accounts.Find(transferDTO.Source);
            Account destinationAccount = context.Accounts.Find(transferDTO.Destination); 
            Customer customer = context.Customers.Find(sourceAccount.CustomerId);

            if (sourceAccount == null || destinationAccount == null)
                return Results.NotFound("There's no such Account with the given Id");

            Transfer transfer = new Transfer(transferDTO.Value, sourceAccount.Id, destinationAccount.Id, customer.Name);

            if (!transfer.IsValid)
                return Results.ValidationProblem(transfer.Notifications.ConvertToProblemDetails());

            sourceAccount.Balance -= transfer.Value;
            destinationAccount.Balance += transfer.Value;

            sourceAccount.Transactions.Add(transfer);
            destinationAccount.Transactions.Add(transfer);
            sourceAccount.EditInfo(sourceAccount.Balance, sourceAccount.Transactions);
            destinationAccount.EditInfo(destinationAccount.Balance, destinationAccount.Transactions);

            context.Transactions.Add(transfer);
            context.SaveChanges();

            return Results.Ok(new TransactionDTO(transfer.Type, transfer.Value, transfer.Tax, transfer.CreatedBy, transfer.CreatedOn, sourceAccount.Balance));
        }
    }
}
