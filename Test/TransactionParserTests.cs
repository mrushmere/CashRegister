using CashRegisterNS;

namespace Test
{
    [TestFixture]
    public class TransactionParserTests
    {
        [Test]
        public void ReadFile()
        {

            var result = TransactionParser.File("./TestFiles/test.txt").Value.ToList();
            Assert.Multiple(() =>
            {
                Assert.That(result[0].amountOwed, Is.EqualTo(2.12m));
                Assert.That(result[0].amountPaid, Is.EqualTo(3.00m));
                Assert.That(result[1].amountOwed, Is.EqualTo(1.97m));
                Assert.That(result[1].amountPaid, Is.EqualTo(2.00m));
                Assert.That(result[2].amountOwed, Is.EqualTo(3.33m));
                Assert.That(result[2].amountPaid, Is.EqualTo(20.00m));
            });
        }

        [Test]
        public void ReadFile_EmptyFile()
        {
            Assert.That(TransactionParser.File("./TestFiles/empty.txt").Value, Is.Empty);
        }

        [Test]
        public void ReadFile_BadFormat()
        {
            var result = TransactionParser.File("./TestFiles/badformat.txt");
            Assert.That(result.Error, Contains.Substring(ParserErrors.InvalidLine));
        }

        [Test]
        public void ReadFile_InvalidAmount()
        {
            var result = TransactionParser.File("./TestFiles/invalidamount.txt");
            Assert.That(result.Error, Contains.Substring(ParserErrors.InvalidAmountPaid));
        }
    }
} 