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
    public class WithdrawController : ControllerBase
    {
        private readonly AccountService _accountService;

        public WithdrawController(AccountService accountService)
        {
            _accountService = accountService;            
        }

        [HttpPost]
        public IResult Withdraw(WithdrawDTO withdrawDTO, ApplicationDBContext context)
        {
            if (withdrawDTO == null)
                return Results.BadRequest();

            if(!withdrawDTO.IsValid)
                return Results.ValidationProblem(withdrawDTO.Notifications.ConvertToProblemDetails());

            Account account = _accountService.Get(withdrawDTO.AccountId);

            if(account.Balance - withdrawDTO.Value < 0)
                return Results.BadRequest("There's no suficient balance to accomplish this withdraw"); ;

            if (account != null)
            {
                CashFlowDTO cashFlow = _accountService.CreateWithdraw(withdrawDTO, account);
                return Results.Ok(cashFlow);
            } 
            else
            {
                return Results.NotFound("There's no such Account with the given Id");
            }
        }
    }
}
