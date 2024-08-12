using CashRegister;
using CashRegister.Currency;

namespace Test
{
    [TestFixture]
    public class CashRegisterTests
    {
        private CashRegister.CashRegister cashRegister;

        [SetUp]
        public void Setup()
        {
            var currency = new USD();
            cashRegister = new CashRegister.CashRegister(currency);
        }

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
            return ChangeListToIntArray(change);
        }


        private static int[] ChangeListToIntArray(List<Tuple<int, Money>> change)
        {
            var result = new int[10];
            result[0] = change.Where(x => x.Item2.Amount == .01m).Select(x => x.Item1).FirstOrDefault();
            result[1] = change.Where(x => x.Item2.Amount == .05m).Select(x => x.Item1).FirstOrDefault();
            result[2] = change.Where(x => x.Item2.Amount == .1m).Select(x => x.Item1).FirstOrDefault();
            result[3] = change.Where(x => x.Item2.Amount == .25m).Select(x => x.Item1).FirstOrDefault();
            result[4] = change.Where(x => x.Item2.Amount == 1m).Select(x => x.Item1).FirstOrDefault();
            result[5] = change.Where(x => x.Item2.Amount == 5m).Select(x => x.Item1).FirstOrDefault();
            result[6] = change.Where(x => x.Item2.Amount == 10m).Select(x => x.Item1).FirstOrDefault();
            result[7] = change.Where(x => x.Item2.Amount == 20m).Select(x => x.Item1).FirstOrDefault();
            result[8] = change.Where(x => x.Item2.Amount == 50m).Select(x => x.Item1).FirstOrDefault();
            result[9] = change.Where(x => x.Item2.Amount == 100m).Select(x => x.Item1).FirstOrDefault();
            return result;

        }

    }
}