namespace RockBank.Domain.DTOs
{
    public class CashFlowDTO
    {
        public CashFlowDTO(string type, double value, double tax, string createdBy, DateTime createdOn, double currentBalance)
        {
            Type = type;
            Value = "R$ " + value;
            Tax = "R$ " + tax;
            CreatedBy = createdBy;
            CreatedOn = createdOn;
            CurrentBalance = "R$ " + currentBalance;
        }

        public string Type { get; private set; }
        public string Tax { get; private set; }
        public string Value { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string CurrentBalance { get; private set; }
    }
}
