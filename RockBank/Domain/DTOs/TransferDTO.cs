namespace RockBank.Domain.DTOs
{
    public class TransferDTO
    {
        public double Value { get; set; }
        public Guid Source { get; set; }
        public Guid Destination { get; set; }
    }
}