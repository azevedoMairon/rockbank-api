namespace RockBank.Domain.Classes.Accounts
{
    public class Customer : Entity
    {
        public Customer()
        {
            
        }
        public Customer(string name, string cpf, string password)
        {
            this.Name = name;
            this.CPF = cpf;
            this.Password = password;          
        }

        public string Name { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
    }
}
