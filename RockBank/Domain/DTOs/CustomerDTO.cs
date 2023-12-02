namespace RockBank.Domain.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO(string name, string cpf, string password)
        {
            Name = name;
            CPF = cpf;
            Password = password;
        }

        public string Name { get; set; }
        public string CPF { get; set; }
        public string Password { get; set; }
    }
}