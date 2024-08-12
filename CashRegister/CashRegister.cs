using CashRegisterNS.Currency;
using System.Configuration;

namespace CashRegisterNS
{
    public class CashRegister(ICurrency currency)
    {
        private readonly ICurrency currency = currency;

        public Dictionary<Money, int> Transaction(decimal amountOwed, decimal amountPaid)
        {
            decimal change = amountPaid - amountOwed;

            if (change < 0)
            {
                throw new Exception("Amount paid is less than amount owed");
            }

            if (bool.TryParse(ConfigurationManager.AppSettings["RandomChange"], out bool randomChange) && randomChange)
            {
                if(int.TryParse(ConfigurationManager.AppSettings["RandomDivisor"], out int randomDivisor) && 
                    (int)(amountOwed*100) % randomDivisor == 0)
                {
                    return RandomChange(change);
                }
            }
            
            return Change(change);
        }


        private Dictionary<Money, int> Change(decimal amount)
        {
            var change = new Dictionary<Money, int>();

            var temp = amount;
            foreach (Money denomination in currency.GetDenominations())
            {
                var count = Convert.ToInt32(Math.Floor(temp / denomination.Amount));
                if (count > 0)
                {
                    change.TryAdd(denomination, count);
                    temp -= count * denomination.Amount;
                }
            }

            return change.OrderBy(x => x.Key.Amount).Reverse().ToDictionary();
        }

        private Dictionary<Money, int> RandomChange(decimal amount)
        {
            // Shuffle the denominations
            var denominations = currency.GetDenominations();

            var change = new Dictionary<Money, int>();

            var temp = amount;
            while (temp > 0)
            {
                // select a random denomination
                denominations = denominations.OrderBy(x => Guid.NewGuid()).ToList();
                var denomination = denominations.First();
                var count = Convert.ToInt32(Math.Floor(temp / denomination.Amount));
                if (count > 0)
                {
                    // add a number between 0 and the count to the change
                    var randomCount = new Random().Next(0, count + 1);
                    if (!change.TryAdd(denomination, randomCount))
                    {
                        change[denomination] += randomCount;   
                    }
                    temp -= randomCount * denomination.Amount;

                } else
                {

                   denominations.Remove(denomination);
                }
            }

            return change.OrderBy(x => x.Key.Amount).Reverse().ToDictionary();
        }
    }
}