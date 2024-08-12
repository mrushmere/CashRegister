using CashRegisterNS;
using CashRegisterNS.Currency;
using System.Configuration;


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

if(!bool.TryParse(ConfigurationManager.AppSettings["RandomChange"], out bool randomChange) ||
    !int.TryParse(ConfigurationManager.AppSettings["RandomDivisor"], out int randomDivisor))
{
    Console.WriteLine("Missing application settings");
    return;
}

var settings= new CashRegisterSettings(randomChange, randomDivisor);

var cashRegister = new CashRegister(new USD(), settings);
var transactions = TransactionParser.File(filename);

foreach (var (amountOwed, amountPaid) in transactions)
{
    try
    {
        var change = cashRegister.Transaction(amountOwed, amountPaid);
        var output = new List<string>(); ;
        foreach (var item in change)
        {
            output.Add($"{item.Value} {item.Key.GetType().Name}");
        }

        Console.WriteLine(string.Join(", ", output));
    }
    catch (Exception ex)
    {
        Console.Write(ex.ToString());
    }
}

