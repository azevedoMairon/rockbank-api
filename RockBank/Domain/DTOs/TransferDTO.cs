using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.Identity.Client;

namespace RockBank.Domain.DTOs
{
    public class TransferDTO : Notifiable<Notification>
    {
        private void Validate()
        {
            var contract = new Contract<WithdrawDTO>()
                .IsNotNull(Value, "Value")
                .IsGreaterThan(Value, 0, "Value")
                .IsNotNull(SourceAccountId, "Source")
                .IsNotNull(DestinationAccountId, "Destination");
            AddNotifications(contract);
        }
        public TransferDTO(double value, Guid source, Guid destination)
        {
            Validate();
            Value = value;
            SourceAccountId = source;
            DestinationAccountId = destination;
        }
        public double Value { get; private set; }
        public Guid SourceAccountId { get; private set; }
        public Guid DestinationAccountId { get; private set; }
    }
}