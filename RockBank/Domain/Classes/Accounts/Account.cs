using Flunt.Validations;
using Newtonsoft.Json.Linq;

namespace RockBank.Domain.Classes.Accounts
{
    public class Account : Entity
    {
        public string Number { get; private set; }
        public double Balance { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        private void Validate()
        {
            var contract = new Contract<Account>()
                .IsNotNull(Number, "Value")
                .IsNotNull(CustomerId, "CustomerId")
                .IsNotNull(Balance, "Balance")
                .IsGreaterThan(Balance, 0, "Balance");
            AddNotifications(contract);
        }

        public Account(string number, double balance, Guid customerId)
        {
            Validate();

            Number = number;
            Balance = balance;
            CustomerId = customerId;
            Transactions = new List<Transaction>();
        }
        public void EditInfo(double balance, List<Transaction> transactions)
        {
            Validate();

            Balance = balance;
            Transactions = transactions;
        }

        public void AddBalance(double value)
        {
            Balance += value;
        }

        public void RemoveBalance(double value)
        {
            Balance -= value;
        }
    }
}
