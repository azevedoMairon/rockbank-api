using RockBank.Domain.Classes.Accounts;

namespace RockBank.Services.Interfaces
{
    public interface IAccountService
    {
        public List<Account> GetAll();

        public Account Get(int id);
    }
}
