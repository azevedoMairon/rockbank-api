using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;

namespace RockBankTests.Domain
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void CustomerNameShouldNotBeNullOrEmpty() {
            string customerName = string.Empty;
            string customerCPF = "111.111.111-11";
            string customerPassword = "123456";

            CustomerDTO customerDTO = new CustomerDTO(customerName, customerCPF, customerPassword);

            Assert.IsNotNull(customerDTO);
            Assert.AreEqual(false, customerDTO.IsValid);        
        }

        [TestMethod]
        public void CustomerCPFShouldNotBeNullOrEmpty()
        {
            string customerName = "Mairon Azevedo";
            string customerCPF = string.Empty;
            string customerPassword = "123456";

            CustomerDTO customerDTO = new CustomerDTO(customerName, customerCPF, customerPassword);

            Assert.IsNotNull(customerDTO);
            Assert.AreEqual(false, customerDTO.IsValid);
        }

        [TestMethod]
        public void CustomerPasswordShouldNotBeNullOrEmpty()
        {
            string customerName = "Mairon Azevedo";
            string customerCPF = "111.111.111-11";
            string customerPassword = string.Empty;

            CustomerDTO customerDTO = new CustomerDTO(customerName, customerCPF, customerPassword);

            Assert.IsNotNull(customerDTO);
            Assert.AreEqual(false, customerDTO.IsValid);
        }

        [TestMethod]
        public void CustomerConstructorShouldCreateCustomer()
        {
            string customerName = "Mairon Azevedo";
            string customerCPF = "111.111.111-11";
            string customerPassword = "123456";

            Customer customer = new Customer(customerName, customerCPF, customerPassword);

            Assert.IsNotNull(customer);
            Assert.AreEqual(customerName, customer.Name);
            Assert.AreEqual(customerCPF, customer.CPF);
            Assert.AreEqual(customerPassword, customer.Password);
        }
    }
}
