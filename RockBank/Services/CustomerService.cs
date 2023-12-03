using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;
using RockBank.Services.Interfaces;

namespace RockBank.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDBContext _context;

        public CustomerService(ApplicationDBContext context)
        {
            _context = context;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer Get(Guid id ) {
            return _context.Customers.Find(id);
        }

        public Customer Create(CustomerDTO customerDTO)
        {
            Customer customer = new Customer(customerDTO.Name, customerDTO.CPF, customerDTO.Password);
            
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }
    }
}
