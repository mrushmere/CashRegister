namespace CashRegisterNS
{
    public static class TransactionParser
    {
        /// <summary>
        /// Parses a file containing transaction data and returns a sequence of tuples representing the amount owed and amount paid for each transaction.
        /// </summary>
        /// <param name="filename">The path to the file to be parsed.</param>
        /// <returns>A sequence of tuples representing the amount owed and amount paid for each transaction.</returns>
        public static IEnumerable<(decimal amountOwed, decimal amountPaid)> File(string filename)
        {
            var lines = System.IO.File.ReadAllLines(filename);
            if (lines.Length == 0)
            {
                throw new Exception("File is empty");
            }

            foreach (string line in lines)
            {
                string[] amounts = line.Split(',');
                if (amounts.Length != 2)
                {
                    throw new Exception("Invalid line: " + line);
                }

                // Parse the amounts, amount owed is the first value, amount paid is the second value.
                if (!decimal.TryParse(amounts[0], out decimal amountOwed))
                {
                    throw new Exception("Invalid amount owed: " + amounts[0]);
                }

                if (!decimal.TryParse(amounts[1], out decimal amountPaid))
                {
                    throw new Exception("Invalid amount paid: " + amounts[1]);
                }

                yield return (amountOwed, amountPaid);
            }
        }
    }
}
