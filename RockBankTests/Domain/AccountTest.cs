using RockBank.Domain.Classes.Accounts;
using RockBank.Domain.DTOs;

namespace RockBankTests.Domain
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void AccountNumberShouldNotBeNullOrEmpty() {
            string accountNumber = "";
            double accountBalance = 5000;
            Guid customerId = Guid.Empty;

            AccountDTO accountDTO = new AccountDTO(accountNumber, accountBalance, customerId);

            Assert.IsNotNull(accountDTO);
            Assert.AreEqual(false, accountDTO.IsValid);        
        }

        [TestMethod]
        public void AccountBalanceShouldNotBeNegative()
        {
            string accountNumber = "7777-77";
            double accountBalance = -3000;
            Guid customerId = Guid.Empty;

            AccountDTO accountDTO = new AccountDTO(accountNumber, accountBalance, customerId);

            Assert.IsNotNull(accountDTO);
            Assert.AreEqual(false, accountDTO.IsValid);
        }

        [TestMethod]
        public void AccountConstructorShouldCreateAccount()
        {
            string accountNumber = "77777-77";
            double accountBalance = 5000;
            Guid customerId = Guid.Empty;

            Account account = new Account(accountNumber, accountBalance, customerId);

            Assert.IsNotNull(account);
            Assert.AreEqual(accountNumber, account.Number);
            Assert.AreEqual(accountBalance, account.Balance);
            Assert.AreEqual(customerId, account.CustomerId);
        }
    }
}
