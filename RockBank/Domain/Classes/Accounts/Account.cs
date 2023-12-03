namespace RockBank.Domain.Classes.Accounts
{
    public class Account : Entity
    {
        public string Number { get; private set; }
        public double Balance { get; private set; }
        public Guid CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Account(string number, double balance, Guid customerId)
        {
            Number = number;
            Balance = balance;
            CustomerId = customerId;
            Transactions = new List<Transaction>();
        }

        public void EditInfo(double balance, List<Transaction> transactions)
        {
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
