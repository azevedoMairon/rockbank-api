using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;

namespace RockBank.Services.Interfaces
{
    public interface ICustomerService
    {
        public List<Customer> GetAll();
        public Customer Get(Guid id);
        public Customer Create(CustomerDTO customerDTO);
    }
}
