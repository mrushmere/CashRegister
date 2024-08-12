using CashRegisterNS;
using CashRegisterNS.Currency;

namespace Test
{
    [TestFixture]
    public class CashRegisterTests
    {
        private readonly CashRegister cashRegister = new(new USD(), new CashRegisterSettings(true, 3));

        [Test]
        public void NotEnoughMoney()
        {
            Assert.Throws<Exception>(() => cashRegister.Transaction(20m, 10m));
        }

        [Test]
        [TestCase(20, 20, ExpectedResult = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
        [TestCase(2.50, 5, ExpectedResult = new int[] { 0, 0, 0, 2, 2, 0, 0, 0, 0, 0 })]
        [TestCase(2.51, 3, ExpectedResult = new int[] { 4, 0, 2, 1, 0, 0, 0, 0, 0, 0 })]
        [TestCase(.01, 100, ExpectedResult = new int[] { 4, 0, 2, 3, 4, 1, 0, 2, 1, 0 })]
        public int[] CorrectChange(decimal price, decimal payment)
        {

            var change = cashRegister.Transaction(price, payment);
            return ChangeDictionaryToIntArray(change);
        }

        [Test]
        public void RandomChangeHasCorrectValue()
        {
            var change = cashRegister.Transaction(3.33m, 5m);
            decimal total = 0;
            foreach(var (denomination, count) in change)
            {
                total += denomination.Amount * count;
            }

            Assert.That(total, Is.EqualTo(1.67m));
        }


        private static int[] ChangeDictionaryToIntArray(Dictionary<Money, int> change)
        {
            var result = new int[10];
            result[0] = change.Where(x => x.Key.Amount == .01m).Select(x => x.Value).FirstOrDefault();
            result[1] = change.Where(x => x.Key.Amount == .05m).Select(x => x.Value).FirstOrDefault();
            result[2] = change.Where(x => x.Key.Amount == .1m).Select(x => x.Value).FirstOrDefault();
            result[3] = change.Where(x => x.Key.Amount == .25m).Select(x => x.Value).FirstOrDefault();
            result[4] = change.Where(x => x.Key.Amount == 1m).Select(x => x.Value).FirstOrDefault();
            result[5] = change.Where(x => x.Key.Amount == 5m).Select(x => x.Value).FirstOrDefault();
            result[6] = change.Where(x => x.Key.Amount == 10m).Select(x => x.Value).FirstOrDefault();
            result[7] = change.Where(x => x.Key.Amount == 20m).Select(x => x.Value).FirstOrDefault();
            result[8] = change.Where(x => x.Key.Amount == 50m).Select(x => x.Value).FirstOrDefault();
            result[9] = change.Where(x => x.Key.Amount == 100m).Select(x => x.Value).FirstOrDefault();
            return result;

        }

    }
}