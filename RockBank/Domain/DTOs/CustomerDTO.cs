using Flunt.Notifications;
using Flunt.Validations;

namespace RockBank.Domain.DTOs
{
    public class CustomerDTO : Notifiable<Notification>
    {
        private void Validate()
        {
            var contract = new Contract<CustomerDTO>()
                .IsNotNull(Name, "Value")
                .IsNotNull(CPF, "CustomerId")
                .IsNotNull(Password, "Balance");
            AddNotifications(contract);
        }

        public CustomerDTO(string name, string cpf, string password)
        {
            Validate();

            Name = name;
            CPF = cpf;
            Password = password;
        }

        public string Name { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
    }
}