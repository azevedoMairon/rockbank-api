using RockBank.Domain.Classes.Accounts;

namespace RockBank.Domain.Classes.Transactions
{
    public class Transfer : Cashflow
    {
        public Transfer(double value, Guid sourceId, Guid destinationId, string createdBy)
        {
            Type = "Transfer";
            Value = CalculateValue(value);
            SourceId = sourceId;
            DestinationId = destinationId;
            CreatedBy = createdBy;
            Tax = this.CalculateTax(value);
        }

        public Guid DestinationId { get; set; }
        public Account Destination { get; set; }

        public override double CalculateTax(double value)
        {
            return 1;
        }

        public override double CalculateValue(double value)
        {
            return value - CalculateTax(value);
        }
    }
}
