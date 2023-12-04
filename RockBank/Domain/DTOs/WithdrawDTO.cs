using Flunt.Notifications;
using Flunt.Validations;

namespace RockBank.Domain.DTOs
{
    public class WithdrawDTO : Notifiable<Notification>
    {
        private void Validate()
        {
            var contract = new Contract<WithdrawDTO>()
                .IsNotNull(Value, "Value")
                .IsGreaterThan(Value, 0, "Value")
                .IsNotNull(AccountId, "AccountId");
            AddNotifications(contract);
        }
        public WithdrawDTO(double value, Guid account)
        {
            Value = value;
            AccountId = account;

            Validate();
        }

        public double Value { get; private set; }
        public Guid AccountId { get; private set; }
    }
}