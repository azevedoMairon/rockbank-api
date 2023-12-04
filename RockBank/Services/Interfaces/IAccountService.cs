using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;

namespace RockBank.Services.Interfaces
{
    public interface IAccountService
    {
        public List<Account> GetAll();
        public Account Get(Guid id);
        public Account Create(AccountDTO accountDTO);
    }
}
