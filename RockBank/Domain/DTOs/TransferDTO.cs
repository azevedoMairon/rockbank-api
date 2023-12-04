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
                .IsNotNull(SourceId, "Source")
                .IsNotNull(DestinationId, "Destination");
            AddNotifications(contract);
        }
        public TransferDTO(double value, Guid source, Guid destination)
        {
            Value = value;
            SourceId = source;
            DestinationId = destination;

            Validate();
        }
        public double Value { get; private set; }
        public Guid SourceId { get; private set; }
        public Guid DestinationId { get; private set; }
    }
}