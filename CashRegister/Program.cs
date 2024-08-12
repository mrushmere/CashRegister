using CashRegisterNS;
using CashRegisterNS.Currency;


Console.WriteLine("Enter filename");
string? filename = Console.ReadLine();
if (filename is null)
{
    Console.WriteLine("Filename is required");
    return;
}

var lines = File.ReadAllLines(filename);
if (lines.Length == 0)
{
    Console.WriteLine("File is empty");
    return;
}

var cashRegister = new CashRegister(new USD());

foreach (string line in lines)
{
    string[] amounts = line.Split(',');
    if (amounts.Length != 2)
    {
        Console.WriteLine("Invalid line: " + line);
        continue;
    }

    // Parse the amounts, amount owed is the first value, amount paid is the second value.
    if (!decimal.TryParse(amounts[0], out decimal amountOwed))
    {
        Console.WriteLine("Invalid amount owed: " + amounts[0]);
        continue;
    }
    if (!decimal.TryParse(amounts[1], out decimal amountPaid))
    {
        Console.WriteLine("Invalid amount paid: " + amounts[1]);
        continue;
    }
    var change = cashRegister.Transaction(amountOwed, amountPaid);

    var output = new List<string>();;
    foreach (var item in change)
    {
        output.Add($"{item.Value} {item.Key.GetType().Name}");
    }

    Console.WriteLine(string.Join(", ", output));
}
