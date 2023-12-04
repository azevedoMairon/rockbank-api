using RockBank.Domain.Classes;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.Classes.Transactions;
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
            Account account = _context.Accounts.Find(id);
            Customer customer = _context.Customers.Find(account.CustomerId);
            List<Cashflow> cashflows = _context.Transactions.Where(t => t.SourceId == id || t.DestinationId == id).ToList();

            account.Customer = customer;
            account.Transactions = cashflows;

            return account;
        }

        public Account Create(AccountDTO accountDTO)
        {
            Account account = new Account(accountDTO.Number, accountDTO.Balance, accountDTO.CustomerId);

            _context.Accounts.Add(account);
            _context.SaveChanges();
            
            return account;
        }

        public void PersistMoneyOperation(Account account, Cashflow cashFlow)
        {
            account.Transactions.Add(cashFlow);

            account.EditInfo(account.Balance, account.Transactions);

            _context.Transactions.Add(cashFlow);
            _context.SaveChanges();
        }

        public CashFlowDTO CreateDeposit(DepositDTO depositDTO, Account account)
        {
            Deposit deposit = new Deposit(depositDTO.Value, depositDTO.AccountId, account.Customer.Name);
            deposit.DestinationId = depositDTO.AccountId;

            account.AddBalance(deposit.Value);
            PersistMoneyOperation(account, deposit);

            return new CashFlowDTO(deposit.Type, deposit.Value, deposit.Tax, deposit.CreatedBy, deposit.CreatedOn, account.Balance);
        }

        public CashFlowDTO CreateWithdraw(WithdrawDTO withdrawDTO, Account account)
        {
            Withdraw withdraw = new Withdraw(withdrawDTO.Value, withdrawDTO.AccountId, account.Customer.Name);
            withdraw.DestinationId = withdrawDTO.AccountId;

            account.RemoveBalance(withdraw.Value);
            PersistMoneyOperation(account, withdraw);

            return new CashFlowDTO(withdraw.Type, withdraw.Value, withdraw.Tax, withdraw.CreatedBy, withdraw.CreatedOn, account.Balance);
        }

        public CashFlowDTO CreateTransfer(TransferDTO transferDTO, Account sourceAccount, Account destinationAccount)
        {
            Transfer transfer = new Transfer(transferDTO.Value, transferDTO.SourceId, transferDTO.DestinationId, sourceAccount.Customer.Name);
            transfer.DestinationId = transferDTO.DestinationId;

            sourceAccount.RemoveBalance(transfer.Value);
            PersistMoneyOperation(sourceAccount, transfer);

            destinationAccount.AddBalance(transfer.Value);
            _context.SaveChanges();

            return new CashFlowDTO(transfer.Type, transfer.Value, transfer.Tax, transfer.CreatedBy, transfer.CreatedOn, sourceAccount.Balance);
        }
    }
}
