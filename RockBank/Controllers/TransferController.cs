using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;
using RockBank.Services;
using RockBank.Utils;
using System.Security.Cryptography.Xml;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferController
    {
        private readonly AccountService _accountService;

        public TransferController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IResult Transfer(TransferDTO transferDTO, ApplicationDBContext context)
        {
            if (transferDTO == null)
                return Results.BadRequest();

            if (!transferDTO.IsValid)
                return Results.ValidationProblem(transferDTO.Notifications.ConvertToProblemDetails());
            
            Account sourceAccount = _accountService.Get(transferDTO.SourceAccountId);
            Account destinationAccount = _accountService.Get(transferDTO.DestinationAccountId);

            if (sourceAccount != null && destinationAccount != null)
            {
                if (sourceAccount.Balance - transferDTO.Value < 0)
                    return Results.BadRequest("There's no suficient balance to accomplish this transfer.");

                CashFlowDTO cashFlow = _accountService.CreateTransfer(transferDTO, sourceAccount, destinationAccount);

                return Results.Ok(cashFlow);
            }
            else
            {
                return Results.NotFound("There's no such Account with the given Id.");
            }          
        }
    }
}
