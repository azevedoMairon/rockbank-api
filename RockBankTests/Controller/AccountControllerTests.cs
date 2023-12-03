using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RockBank.Controllers;
using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;
using RockBank.Infra.Data;

namespace RockBankTests.Controller
{
    [TestClass]
    public class AccountControllerTests
    {
        private DbContextOptions<ApplicationDBContext> _options;
        private CustomerController _customerController;
        private AccountController _accountController;
        private ApplicationDBContext _dbContext;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryAccountDB")
                .Options;

            _dbContext = new ApplicationDBContext(_options);
            _customerController = new CustomerController();
            _accountController = new AccountController();
        }

        [TestMethod]
        public void AccountShouldNotBeNull()
        {
            AccountDTO accountDTO = null;

            BadRequest result = (BadRequest)_accountController.Create(accountDTO, _dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void CustomerMustExist()
        {
            AccountDTO accountDTO = new AccountDTO("77777-77", 3000, new Guid());

            NotFound<String> result = (NotFound<String>)_accountController.Create(accountDTO, _dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public void ControllerShouldCreateAccount()
        {
            CustomerDTO customerDTO = new CustomerDTO("Mairon Azevedo", "111-111-111-11", "123456");
            Created<Customer> customerResult = (Created<Customer>)_customerController.Create(customerDTO, _dbContext);

            AccountDTO accountDTO = new AccountDTO("77777-77", 5000, customerResult.Value.Id);

            Created<Account> result = (Created<Account>)_accountController.Create(accountDTO, _dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
            Assert.AreEqual(accountDTO.Number, result.Value.Number);
            Assert.AreEqual(accountDTO.Balance, result.Value.Balance);
            Assert.AreEqual(accountDTO.CustomerId, result.Value.CustomerId);
        }

        [TestMethod]
        public void ControllerShouldReturnAllAccounts()
        {
            CustomerDTO customerDTO = new CustomerDTO("Mairon Azevedo", "111-111-111-11", "123456");
            Created<Customer> customerResult = (Created<Customer>)_customerController.Create(customerDTO, _dbContext);

            AccountDTO account1 = new AccountDTO("77777-77", 5000, customerResult.Value.Id);
            AccountDTO account2 = new AccountDTO("23626-37", 3000, customerResult.Value.Id);

            _accountController.Create(account1, _dbContext);
            _accountController.Create(account2, _dbContext);

            Ok<List<Account>> result = (Ok<List<Account>>)_accountController.Read(_dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(List<Account>));

            List<Account> account = result.Value;
            Assert.IsNotNull(account);
            Assert.AreEqual(2, account.Count());
        }

        [TestMethod]
        public void ControllerShoudReturnEmptyList()
        {
            Ok<List<Account>> result = (Ok<List<Account>>)_accountController.Read(_dbContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);

            Assert.IsNotNull(result.Value);
            Assert.IsInstanceOfType(result.Value, typeof(List<Account>));

            List<Account> account = result.Value;

            Assert.AreEqual(0, account.Count);
        }
    }
}
