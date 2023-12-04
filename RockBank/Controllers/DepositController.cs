using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;
using RockBank.Services;
using RockBank.Utils;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepositController : ControllerBase
    {
        private readonly AccountService _accountService;

        public DepositController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IResult Deposit(DepositDTO depositDTO)
        {
            if (depositDTO == null)
                return Results.BadRequest();
            
            if(!depositDTO.IsValid) 
                return Results.ValidationProblem(depositDTO.Notifications.ConvertToProblemDetails());

            Account account = _accountService.Get(depositDTO.AccountId);

            if (account != null)
            {
                CashFlowDTO cashFlow = _accountService.CreateDeposit(depositDTO, account);
                return Results.Ok(cashFlow);
            } 
            else
            {
                return Results.NotFound("There's no such Account with the given Id");
            }
        }
    }
}
