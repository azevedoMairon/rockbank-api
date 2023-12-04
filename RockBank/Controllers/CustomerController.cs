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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IResult Create(CustomerDTO customerDTO)
        {
            if(customerDTO == null)
                return Results.BadRequest();

            if (!customerDTO.IsValid)
                return Results.ValidationProblem(customerDTO.Notifications.ConvertToProblemDetails());

            Customer customer = _customerService.Create(customerDTO);

            return Results.Created($"account/{customer.Id}", customer);
        }

        [HttpGet]
        public IResult Read()
        {
            var customers = _customerService.GetAll();
            return Results.Ok(customers);
        }
    }
}
