using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public IResult GetAll(ApplicationDBContext context)
        {
            var customers = context.Customers.ToList();
            return Results.Ok(customers);
        }

        [HttpPost]
        public IResult Post(CustomerDTO customerDTO, ApplicationDBContext context)
        {
            if(customerDTO == null)
                return Results.BadRequest();

            var customer = new Customer
            {
                Name = customerDTO.Name,
                CPF = customerDTO.CPF,
                Password = customerDTO.Password
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            return Results.Created($"account/{customer.Id}", customer);
        }
    }
}
