using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockBank.Controllers;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;

namespace RockBankTests
{
    [TestClass]
    public class CustomerControllerTests
    {
        private DbContextOptions<ApplicationDBContext> _options;
        private ApplicationDBContext _dbContext;
        private CustomerController _customerController;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryCustomerDB")
                .Options;

            _dbContext = new ApplicationDBContext(_options);
            _customerController = new CustomerController();
        }

        [TestMethod]
        public void CustomerShouldNotBeNull()
        {
            CustomerDTO customerDTO = null;

            BadRequest result = (BadRequest)_customerController.Post(customerDTO, _dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void ControllerShouldCreateCustomer()
        {
            CustomerDTO customerDTO = new CustomerDTO("Mairon Azevedo", "111-111-111-11", "123456");

            Created<Customer> result = (Created<Customer>)_customerController.Post(customerDTO, _dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
            Assert.AreEqual(customerDTO.Name, result.Value.Name);
            Assert.AreEqual(customerDTO.CPF, result.Value.CPF);
        }

        [TestMethod]
        public void ControllerShouldReturnAllCustomers()
        {
            CustomerDTO customer1 = new CustomerDTO("Mairon Azevedo", "111-111-111-11", "123456");
            CustomerDTO customer2 = new CustomerDTO("Denerson Eduardo", "111-111-111-11", "123456");

            _customerController.Post(customer1, _dbContext);
            _customerController.Post(customer2, _dbContext);

            Ok<List<Customer>> result = (Ok<List<Customer>>)_customerController.GetAll(_dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(List<Customer>));

            List<Customer> customers  = (List<Customer>)result.Value;
            Assert.IsNotNull(customers);
            Assert.AreEqual(2, customers.Count());

        }

        [TestMethod]
        public void ControllerShoudReturnEmptyList()
        {
            Ok<List<Customer>> result = (Ok<List<Customer>>)_customerController.GetAll(_dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(List<Customer>));

            List<Customer> customers = (List<Customer>)result.Value;

            Assert.AreEqual(0, customers.Count);
        }
    }
}