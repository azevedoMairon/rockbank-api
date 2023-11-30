namespace RockBank.Domain.DTOs
{
    public class DepositDTO
    {
        public double Value { get; set; }
        public Guid Account { get; set; }
    }
}