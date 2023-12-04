using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;

namespace RockBankTests.Domain
{
    [TestClass]
    public class DepositTest
    {
        private Deposit _createValidDeposit(double depositValue)
        {
            return new Deposit(depositValue, Guid.Empty, "Teste");
        }

        [TestMethod]
        public void DepositValueShouldNotBeNegative() {
            double depositValue = -500;
            Guid depositAccountId = Guid.Empty;

            DepositDTO depositDTO = new DepositDTO(depositValue, depositAccountId);

            Assert.IsNotNull(depositDTO);
            Assert.AreEqual(false, depositDTO.IsValid);        
        }

        [TestMethod]
        public void DepositConstructorShouldCreateDeposit()
        {
            double depositValue = 500;
            Guid depositAccountId = Guid.Empty;
            string createdBy = "Mairon Azevedo";

            Deposit deposit = new Deposit(depositValue, depositAccountId, createdBy);

            Assert.IsNotNull(deposit);
            Assert.AreEqual(depositValue, deposit.Value + deposit.Tax);
            Assert.AreEqual(depositAccountId, deposit.SourceId);
        }

        [TestMethod]
        public void ShouldCalculateTheCorrectTax()
        {
            double depositValue = 100;
            double finalValue = 99;
            double TaxValue = 1;

            Deposit deposit = _createValidDeposit(depositValue);

            Assert.IsNotNull(deposit);
            Assert.AreEqual(TaxValue, deposit.Tax);
            Assert.AreEqual(finalValue, deposit.Value);
        }

        [TestMethod]
        public void ShouldCalculateTheCorrectTaxWithDoublePrecision()
        {
            double depositValue = 105.5;
            double finalValue = 104.45;
            double TaxValue = 1.05;

            Deposit deposit = _createValidDeposit(depositValue);

            Assert.IsNotNull(deposit);
            Assert.AreEqual(TaxValue, deposit.Tax);
            Assert.AreEqual(finalValue, deposit.Value);
        }
    }
}
