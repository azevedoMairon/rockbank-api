namespace RockBank.Domain.Classes.Transactions
{
    public class Transfer : Transaction
    {
        public Transfer()
        {
            Type = "Transfer";
        }

        public string Destination { get; set; }

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
