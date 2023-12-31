using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockBank.Controllers;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;
using RockBank.Services;

namespace RockBankTests.Controller
{
    [TestClass]
    public class CustomerControllerTests
    {
        private DbContextOptions<ApplicationDBContext> _options;
        private CustomerController _customerController;
        private CustomerService _customerService;
        private ApplicationDBContext _dbContext;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryCustomerDB")
                .Options;

            _dbContext = new ApplicationDBContext(_options);
            _customerService = new CustomerService(_dbContext);
            _customerController = new CustomerController(_customerService);
        }

        [TestMethod]
        public void CustomerShouldNotBeNull()
        {
            CustomerDTO customerDTO = null;

            BadRequest result = (BadRequest)_customerController.Create(customerDTO);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void ControllerShouldCreateCustomer()
        {
            CustomerDTO customerDTO = new CustomerDTO("Mairon Azevedo", "111-111-111-11", "123456");

            Created<Customer> result = (Created<Customer>)_customerController.Create(customerDTO);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
            Assert.AreEqual(customerDTO.Name, result.Value.Name);
            Assert.AreEqual(customerDTO.CPF, result.Value.CPF);
        }

        [TestMethod]
        public void ControllerShouldReturnAllCustomers()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            CustomerDTO customer1 = new CustomerDTO("Mairon Azevedo", "111-111-111-11", "123456");
            CustomerDTO customer2 = new CustomerDTO("Denerson Eduardo", "111-111-111-11", "123456");

            _customerController.Create(customer1);
            _customerController.Create(customer2);

            Ok<List<Customer>> result = (Ok<List<Customer>>)_customerController.Read();

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(List<Customer>));

            List<Customer> customers = result.Value;
            Assert.IsNotNull(customers);
            Assert.AreEqual(2, customers.Count);
        }

        [TestMethod]
        public void ControllerShoudReturnEmptyList()
        {
            Ok<List<Customer>> result = (Ok<List<Customer>>)_customerController.Read();

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(List<Customer>));

            List<Customer> customers = result.Value;

            Assert.AreEqual(0, customers.Count);
        }
    }
}