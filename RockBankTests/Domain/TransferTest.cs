using RockBank.Domain.Classes.Transactions;
using RockBank.Domain.DTOs;

namespace RockBankTests.Domain
{
    [TestClass]
    public class TransferTest
    {
        private Transfer _createValidTransfer(double transferValue)
        {
            return new Transfer(transferValue, Guid.Empty, Guid.Empty, "Teste");
        }

        [TestMethod]
        public void TransferValueShouldNotBeNegative() {
            double transferValue = -500;
            Guid transferSourceAccountId = Guid.Empty;
            Guid transferDestinationAccountId = Guid.Empty;
             
            TransferDTO transferDTO = new TransferDTO(transferValue, transferSourceAccountId, transferDestinationAccountId);

            Assert.IsNotNull(transferDTO);
            Assert.AreEqual(false, transferDTO.IsValid);        
        }

        [TestMethod]
        public void TransferConstructorShouldCreateTransfer()
        {
            double transferValue = 500;
            Guid transferSourceAccountId = Guid.Empty;
            Guid transferDestinationAccountId = Guid.Empty;
            string createdBy = "Mairon Azevedo";

            Transfer transfer = new Transfer(transferValue, transferSourceAccountId, transferDestinationAccountId, createdBy);

            Assert.IsNotNull(transfer);
            Assert.AreEqual(transferValue, transfer.Value + transfer.Tax);
            Assert.AreEqual(transferSourceAccountId, transfer.SourceId);
        }

        [TestMethod]
        public void ShouldCalculateTheCorrectTax()
        {
            double transferValue = 100;
            double finalValue = 99;
            double TaxValue = 1;

            Transfer transfer = _createValidTransfer(transferValue);

            Assert.IsNotNull(transfer);
            Assert.AreEqual(TaxValue, transfer.Tax);
            Assert.AreEqual(finalValue, transfer.Value);
        }

        [TestMethod]
        public void ShouldCalculateTheCorrectTaxWithDoublePrecision()
        {
            double transferValue = 105.5;
            double finalValue = 104.5;
            double TaxValue = 1;

            Transfer transfer = _createValidTransfer(transferValue);

            Assert.IsNotNull(transfer);
            Assert.AreEqual(TaxValue, transfer.Tax);
            Assert.AreEqual(finalValue, transfer.Value);
        }
    }
}
