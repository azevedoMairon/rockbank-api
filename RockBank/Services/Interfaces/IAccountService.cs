using RockBank.Domain.Classes;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;

namespace RockBank.Services.Interfaces
{
    public interface IAccountService
    {
        public List<Account> GetAll();
        public Account Get(Guid id);
        public Account Create(AccountDTO accountDTO);
        public void PersistMoneyOperation(Account account, Cashflow cashFlow);
        public CashFlowDTO CreateDeposit(DepositDTO depositDTO, Account account);
        public CashFlowDTO CreateWithdraw(WithdrawDTO withdrawDTO, Account account);
        public CashFlowDTO CreateTransfer(TransferDTO transferDTO, Account sourceAccount, Account destinationAccount);
    }
}
