namespace RockBank.Domain.Classes.Transactions
{
    public class Withdraw : Cashflow
    {
        public Withdraw()
        {
            Type = "Withdraw";
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
