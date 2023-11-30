namespace RockBank.Domain.DTOs
{
    public class AccountDTO
    {
        public string Number { get; set; }
        public float Balance { get; set; }
        public Guid CustomerId { get; set; }
    }
}