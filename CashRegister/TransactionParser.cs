using CashRegisterNS.Currency;

namespace CashRegisterNS
{
    public static class TransactionParser
    {
        /// <summary>
        /// Parses a file containing transaction data and returns a sequence of tuples representing the amount owed and amount paid for each transaction.
        /// </summary>
        /// <param name="filename">The path to the file to be parsed.</param>
        /// <returns>A sequence of tuples representing the amount owed and amount paid for each transaction.</returns>
        public static ParserResult File(string filename)
        {
            var result = new List<(decimal, decimal)>();

            foreach (var line in System.IO.File.ReadLines(filename))
            {
                string[] amounts = line.Split(',');
                if (amounts.Length != 2)
                {
                    return new ParserResult { Error = $"{ParserErrors.InvalidLine}: " + line };
                }

                // Parse the amounts, amount owed is the first value, amount paid is the second value.
                if (!decimal.TryParse(amounts[0], out decimal amountOwed))
                {
                    return new ParserResult { Error = $"{ParserErrors.InvalidAmountOwed}: " + amounts[0] };
                }

                if (!decimal.TryParse(amounts[1], out decimal amountPaid))
                {
                    return new ParserResult { Error = $"{ParserErrors.InvalidAmountPaid}: " + amounts[1] };
                }
                result.Add((amountOwed, amountPaid));             
            }
            return new ParserResult { Value = result };
        }
    }
}
