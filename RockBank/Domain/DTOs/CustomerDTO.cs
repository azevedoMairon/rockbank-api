﻿using Flunt.Notifications;
using Flunt.Validations;

namespace RockBank.Domain.DTOs
{
    public class CustomerDTO : Notifiable<Notification>
    {
        private void Validate()
        {
            var contract = new Contract<CustomerDTO>()
                .IsNotNullOrEmpty(Name, "Name")
                .IsNotNullOrEmpty(CPF, "CPF")
                .IsNotNullOrEmpty(Password, "Password");
            AddNotifications(contract);
        }

        public CustomerDTO(string name, string cpf, string password)
        {
            Name = name;
            CPF = cpf;
            Password = password;
            
            Validate();
        }

        public string Name { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
    }
}