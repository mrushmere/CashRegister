using CashRegisterNS.Currency;

namespace CashRegisterNS
{
    /// <summary>
    /// Represents a cash register that handles transactions and calculates change.
    /// </summary>
    public class CashRegister(ICurrency currency, ICashRegisterSettings settings)
    {
        public readonly ICurrency currency = currency;

        public Result Transaction(decimal amountOwed, decimal amountPaid)
        {
            if (amountPaid < 0 || amountOwed < 0)
            {
               return new Result { Error = CashRegisterErrors.NegativeAmount};
                /// <summary>
                /// Performs a transaction in the cash register by calculating the change based on the amount owed and the amount paid.
                /// </summary>
                /// <param name="amountOwed">The amount owed in the transaction.</param>
                /// <param name="amountPaid">The amount paid in the transaction.</param>
                /// <returns>A Result object containing either the calculated change or an error if the transaction is invalid.</returns>
            }

            decimal change = amountPaid - amountOwed;
            if (change < 0)
            {
                return new Result { Error = CashRegisterErrors.AmountPaidLessThanOwed };
            }

            // Check that random change is enabled, the divisor is set, and that the amount owed is divisible by the divisor.
            if (settings.RandomChange && settings.RandomDivisor != null && (int)(amountOwed * 100) % settings.RandomDivisor == 0)
            {
                return new Result { Value = RandomChange(change) };
            }

            return new Result { Value = Change(change) };
        }


        /// <summary>
        /// Generates the change using the available denominations.
        /// </summary>
        /// <param name="amount">The amount of change to be generated.</param>
        /// <returns>A dictionary representing the change, where the key is the denomination and the value is the count.</returns>
        private Dictionary<Money, int> Change(decimal amount)
        {
            var change = new Dictionary<Money, int>();

            if (amount <= 0)
            {
                return change;
            }

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
            var change = new Dictionary<Money, int>();

            if (amount <= 0)
            {
                return change;
            }

            // Shuffle the denominations.
            var denominations = currency.GetDenominations().Where(d => d.Amount <= amount).ToList();

            var temp = amount;
            while (temp > 0)
            {
                // Select a random denomination.
                denominations = denominations.OrderBy(x => Guid.NewGuid()).ToList();
                var denomination = denominations[0];
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


        /// <summary>
        /// Gets the text representation of the change given in a transaction.
        /// </summary>
        /// <param name="change">The dictionary representing the change, where the key is the denomination and the value is the count.</param>
        /// <returns>A string representing the change in the format of "count, denomination".</returns>
        public static string GetChangeText(Dictionary<Money, int> change)
        {
            if (change.Count == 0)
            {
                return Constants.NoChangeDue;
            }
            var output = new List<string>();

            foreach (var item in change)
            {
                if (item.Value > 1)
                {
                    output.Add($"{item.Value} {item.Key.PluralName}");
                }
                else
                {
                    output.Add($"{item.Value} {item.Key.GetType().Name}");
                }
            }

            return string.Join(", ", output);
        }
    }
}