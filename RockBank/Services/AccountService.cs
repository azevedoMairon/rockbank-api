using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;
using RockBank.Services.Interfaces;

namespace RockBank.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDBContext _context;

        public AccountService( ApplicationDBContext context )
        {
            _context = context;
        }

        public List<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }
        public Account Get( Guid id ) {
            return _context.Accounts.Find(id);
        }

        public Account Create(AccountDTO accountDTO)
        {
            Account account = new Account(accountDTO.Number, accountDTO.Balance, accountDTO.CustomerId);

            _context.Accounts.Add(account);
            _context.SaveChanges();
            
            return account;
        }
    }
}
