using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;

namespace RockBank.Controllers
{
    public class TransferController
    {
        [HttpPost]
        public IResult Transfer(TransferDTO transferDTO, ApplicationDBContext context)
        {
            return Results.Ok("Realizando transferência");
        }
    }
}
