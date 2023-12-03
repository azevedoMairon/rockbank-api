using Microsoft.AspNetCore.Mvc;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;
using RockBank.Services;

namespace RockBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly CustomerService _customerService;

        public CustomerController(ApplicationDBContext context, CustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        [HttpPost]
        public IResult Create(CustomerDTO customerDTO)
        {
            if(customerDTO == null)
                return Results.BadRequest();

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
