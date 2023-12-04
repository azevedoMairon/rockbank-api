using Flunt.Notifications;
using Flunt.Validations;
using RockBank.Domain.Classes.Accounts;

namespace RockBank.Domain.DTOs
{
    public class AccountDTO : Notifiable<Notification>
    {
        private void Validate()
        {
            var contract = new Contract<AccountDTO>()
                .IsNotNull(Number, "Value")
                .IsNotNull(CustomerId, "CustomerId")
                .IsNotNull(Balance, "Balance")
                .IsGreaterThan(Balance, 0, "Balance");
            AddNotifications(contract);
        }

        public AccountDTO(string number, float balance, Guid customerId)
        {
            Validate();

            Number = number;
            Balance = balance;
            CustomerId = customerId; 
        }

        public string Number { get; private set; }
        public float Balance { get; private set; }
        public Guid CustomerId { get; set; }
    }
}