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
        public TransferDTO(double value, Guid sourceId, Guid destinationId)
        {
            Value = value;
            SourceId = sourceId;
            DestinationId = destinationId;

            Validate();
        }
        public double Value { get; set; }
        public Guid SourceId { get; set; }
        public Guid DestinationId { get; set; }
    }
}