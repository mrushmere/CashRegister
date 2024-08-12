using CashRegisterNS;
using CashRegisterNS.Currency;

namespace Test
{
    [TestFixture]
    public class CashRegisterTests
    {
        private readonly CashRegister cashRegister = new(new USD(), new CashRegisterSettings(false, 0));
        private readonly CashRegister randomCashRegister = new(new USD(), new CashRegisterSettings(true, 3));

        [Test]
        public void NotEnoughMoney()
        {
            Assert.Throws<Exception>(() => cashRegister.Transaction(20m, 10m));
        }

        [Test]
        public void AmountsMustBePositive()
        {
            Assert.Throws<Exception>(() => cashRegister.Transaction(-20m, 10m));
            Assert.Throws<Exception>(() => cashRegister.Transaction(20m, -10m));
        }

        [Test]
        [TestCase(1, 1, ExpectedResult = Constants.NoChangeDue)]
        [TestCase(.99, 1, ExpectedResult = "1 Penny")]
        [TestCase(.95, 1, ExpectedResult = "1 Nickel")]
        [TestCase(.9, 1, ExpectedResult = "1 Dime")]
        [TestCase(.75, 1, ExpectedResult = "1 Quarter")]
        [TestCase(19.98, 20, ExpectedResult = "2 Pennies")]
        [TestCase(19, 20, ExpectedResult = "1 Dollar")]
        [TestCase(2, 5, ExpectedResult = "3 Dollars")]
        [TestCase(5, 10, ExpectedResult = "1 Five")]
        [TestCase(10, 20, ExpectedResult = "1 Ten")]
        [TestCase(20, 40, ExpectedResult = "1 Twenty")]
        [TestCase(20, 50, ExpectedResult = "1 Twenty, 1 Ten")]
        [TestCase(20, 60, ExpectedResult = "2 Twenties")]
        [TestCase(50, 100, ExpectedResult = "1 Fifty")]
        [TestCase(100, 200, ExpectedResult = "1 Hundred")]   
        public string CorrectChange(decimal price, decimal payment)
        {
            var denominations = cashRegister.Transaction(price, payment);  
            return CashRegister.GetChangeText(denominations);
        }

        [Test]
        public void RandomChangeHasCorrectValue()
        {
            var change = randomCashRegister.Transaction(3.33m, 5m);
            decimal total = 0;
            foreach(var (denomination, count) in change)
            {
                total += denomination.Amount * count;
            }

            Assert.That(total, Is.EqualTo(1.67m));
        }
    }
}