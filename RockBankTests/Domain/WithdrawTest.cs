using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;

namespace RockBankTests.Domain
{
    [TestClass]
    public class WithdrawTest
    {
        private Withdraw _createValidWithdraw(double withdrawValue)
        {
            return new Withdraw(withdrawValue, Guid.Empty, "Teste");
        }

        [TestMethod]
        public void WithdrawValueShouldNotBeNegative() {
            double withdrawValue = -500;
            Guid withdrawAccountId = Guid.Empty;

            WithdrawDTO withdrawDTO = new WithdrawDTO(withdrawValue, withdrawAccountId);

            Assert.IsNotNull(withdrawDTO);
            Assert.AreEqual(false, withdrawDTO.IsValid);        
        }

        [TestMethod]
        public void WithdrawConstructorShouldCreateWithdraw()
        {
            double withdrawValue = 500;
            Guid withdrawAccountId = Guid.Empty;
            string createdBy = "Mairon Azevedo";

            Withdraw withdraw = new Withdraw(withdrawValue, withdrawAccountId, createdBy);

            Assert.IsNotNull(withdraw);
            Assert.AreEqual(withdrawValue, withdraw.Value + withdraw.Tax);
            Assert.AreEqual(withdrawAccountId, withdraw.SourceId);
        }

        [TestMethod]
        public void ShouldCalculateTheCorrectTax()
        {
            double withdrawValue = 100;
            double finalValue = 96;
            double TaxValue = 4;

            Withdraw withdraw = _createValidWithdraw(withdrawValue);

            Assert.IsNotNull(withdraw);
            Assert.AreEqual(TaxValue, withdraw.Tax);
            Assert.AreEqual(finalValue, withdraw.Value);
        }

        [TestMethod]
        public void ShouldCalculateTheCorrectTaxWithDoublePrecision()
        {
            double withdrawValue = 105.5;
            double finalValue = 101.5;
            double TaxValue = 4;

            Withdraw withdraw = _createValidWithdraw(withdrawValue);

            Assert.IsNotNull(withdraw);
            Assert.AreEqual(TaxValue, withdraw.Tax);
            Assert.AreEqual(finalValue, withdraw.Value);
        }
    }
}
