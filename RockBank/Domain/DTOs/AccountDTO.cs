namespace RockBank.Domain.DTOs
{
    public class AccountDTO
    {
        public AccountDTO(string number, float balance, Guid customerId)
        {
            Number = number;
            Balance = balance;
            CustomerId = customerId; 
        }

        public string Number { get; private set; }
        public float Balance { get; private set; }
        public Guid CustomerId { get; set; }
    }
}