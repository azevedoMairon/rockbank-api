using Flunt.Notifications;
using Flunt.Validations;

namespace RockBank.Domain.DTOs
{
    public class DepositDTO : Notifiable<Notification>
    {
        private void Validate()
        {
            var contract = new Contract<DepositDTO>()
                .IsNotNull(Value, "Value")
                .IsGreaterThan(Value, 0, "Value")
                .IsNotNull(AccountId, "AccountId");
            AddNotifications(contract);
        }
        public DepositDTO(double value, Guid accountId)
        {
            Value = value;
            AccountId = accountId;

            Validate();
        }

        public double Value { get; set; }
        public Guid AccountId { get; set; }
    }
}