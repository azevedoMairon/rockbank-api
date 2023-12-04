using Flunt.Validations;
using Microsoft.Identity.Client;
using System.Diagnostics.Contracts;

namespace RockBank.Domain.Classes.Transactions
{
    public class Deposit : Cashflow
    {
        public Deposit(double value, Guid sourceId, string createdBy)
        {
            Type = "Deposit";
            Value = CalculateValue(value);
            SourceId = sourceId;
            CreatedBy = createdBy;
            Tax = this.CalculateTax(value);
        }
        public override double CalculateTax(double value)
        {
            return value * 0.01;
        }
        public override double CalculateValue(double value)
        {
            return value - CalculateTax(value);
        }
    }
}
