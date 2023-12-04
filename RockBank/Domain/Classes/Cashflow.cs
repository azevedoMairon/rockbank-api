using Flunt.Validations;

namespace RockBank.Domain.Classes
{
    public abstract class Cashflow : Entity
    {
        public Cashflow()
        {
            CreatedOn = DateTime.Now;
        }
        public double Tax { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public Guid SourceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Guid DestinationId { get; set; }
        public abstract double CalculateTax(double value);
        public abstract double CalculateValue(double value);
    }
}
