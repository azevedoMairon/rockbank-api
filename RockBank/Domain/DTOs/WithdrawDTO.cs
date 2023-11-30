namespace RockBank.Domain.DTOs
{
    public class WithdrawDTO
    {
        public double Value { get; set; }
        public Guid Account { get; set; }
    }
}