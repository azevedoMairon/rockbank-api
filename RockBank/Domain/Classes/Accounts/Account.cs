namespace RockBank.Domain.Classes.Accounts
{
    public class Account : Entity
    {
        public string Number { get; set; }
        public double Balance { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }

        public void EditInfo(double balance, List<Transaction> transactions)
        {
            Balance = balance;
            Transactions = transactions;
        }
    }
}
