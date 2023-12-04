namespace RockBank.Domain.Classes.Transactions
{
    public class Withdraw : Cashflow
    {
        public Withdraw(double value, Guid sourceId, string createdBy)
        {
            Type = "Withdraw";
            Value = CalculateValue(value);
            SourceId = sourceId;
            CreatedBy = createdBy;
            Tax = CalculateTax(value);
        }
        public override double CalculateTax(double value)
        {
            return 4;
        }
        public override double CalculateValue(double value)
        {
            return value - CalculateTax(value);
        }
    }
}
