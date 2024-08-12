using CashRegisterNS.Currency;
using System.Configuration;

namespace CashRegisterNS
{
    /// <summary>
    /// Represents a cash register that handles transactions and calculates change.
    /// </summary>
    public class CashRegister(ICurrency currency, ICashRegisterSettings settings)
    {
        public readonly ICurrency currency = currency;

        /// <summary>
        /// Calculates the change to be given based on the amount owed and the amount paid.
        /// </summary>
        /// <param name="amountOwed">The amount owed.</param>
        /// <param name="amountPaid">The amount paid.</param>
        /// <returns>A dictionary representing the change, where the key is the denomination and the value is the count.</returns>
        public Dictionary<Money, int> Transaction(decimal amountOwed, decimal amountPaid)
        {
            decimal change = amountPaid - amountOwed;
            if (change < 0)
            {
                throw new Exception("Amount paid is less than amount owed");
            }

            // Check that random change is enabled, the divisor is set, and that the amount owed is divisible by the divisor.
            if (settings.RandomChange && settings.RandomDivisor != null && (int)(amountOwed * 100) % settings.RandomDivisor == 0)
            {
                return RandomChange(change);
            }

            return Change(change);
        }


        /// <summary>
        /// Generates the change using the available denominations.
        /// </summary>
        /// <param name="amount">The amount of change to be generated.</param>
        /// <returns>A dictionary representing the change, where the key is the denomination and the value is the count.</returns>
        private Dictionary<Money, int> Change(decimal amount)
        {
            var change = new Dictionary<Money, int>();

            var temp = amount;
            foreach (Money denomination in currency.GetDenominations().Where(d => d.Amount <= amount))
            {
                var count = Convert.ToInt32(Math.Floor(temp / denomination.Amount));
                if (count > 0)
                {
                    change.TryAdd(denomination, count);
                    temp -= count * denomination.Amount;
                }
            }

            return change;
        }

        /// <summary>
        /// Generates random change using the available denominations.
        /// </summary>
        /// <param name="amount">The amount of change to be generated.</param>
        /// <returns>A dictionary representing the change, where the key is the denomination and the value is the count.</returns>
        private Dictionary<Money, int> RandomChange(decimal amount)
        {
            // Shuffle the denominations.
            var denominations = currency.GetDenominations().Where(d => d.Amount <= amount).ToList();

            var change = new Dictionary<Money, int>();

            var temp = amount;
            while (temp > 0)
            {
                // Select a random denomination.
                denominations = denominations.OrderBy(x => Guid.NewGuid()).ToList();
                var denomination = denominations.First();
                var count = Convert.ToInt32(Math.Floor(temp / denomination.Amount));
                if (count > 0)
                {
                    // Add a random count between 0 and the maximum count to the change.
                    var randomCount = new Random().Next(0, count + 1);
                    if (!change.TryAdd(denomination, randomCount))
                    {
                        change[denomination] += randomCount;
                    }
                    temp -= randomCount * denomination.Amount;
                }
                else
                {
                    denominations.Remove(denomination);
                }
            }

            return change.Where(x => x.Value > 0).OrderBy(x => x.Key.Amount).Reverse().ToDictionary();
        }
    }
}