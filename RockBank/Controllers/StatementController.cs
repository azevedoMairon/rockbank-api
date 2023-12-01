using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatementController : ControllerBase
    {
        [HttpGet]
        public IResult Read(Guid accountId, ApplicationDBContext context)
        {
            if (context.Accounts.Find(accountId) == null)
                return Results.NotFound("There's no such Account with the given Id");

            List<Transaction> transactions = context.Transactions.Where(t => t.SourceId == accountId || t.DestinationId == accountId).ToList();

            return Results.Ok(transactions);
        }
    }
}
