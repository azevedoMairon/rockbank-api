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
                .IsNotNullOrEmpty(Number, "Number")
                .IsNotNull(Balance, "Balance")
                .IsGreaterOrEqualsThan(Balance, 0, "Balance")
                .IsNotNull(CustomerId, "CustomerId");
            AddNotifications(contract);
        }

        public AccountDTO(string number, double balance, Guid customerId)
        {
            Number = number;
            Balance = balance;
            CustomerId = customerId; 
            Validate();
        }

        public string Number { get; private set; }
        public double Balance { get; private set; }
        public Guid CustomerId { get; set; }
    }
}