namespace RockBank.Domain.Classes.Accounts
{
    public class Customer : Entity
    {
        public string Name { get; private set; }
        public string CPF { get; private set; }
        public string Password { get; private set; }
        public Customer()
        {
            
        }
        public Customer(string name, string cpf, string password)
        {
            this.Name = name;
            this.CPF = cpf;
            this.Password = password;          
        }
    }
}
