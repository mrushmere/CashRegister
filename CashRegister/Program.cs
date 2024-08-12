using CashRegisterNS;
using CashRegisterNS.Currency;
using System.Configuration;


Console.WriteLine("Enter full path of input file.");
string? filename = Console.ReadLine();
if (filename is null)
{
    Console.WriteLine("File is required.");
    return;
}

Console.WriteLine("Enter full path of file to write results.");
string? outputFilename = Console.ReadLine();
if (outputFilename is null)
{
    Console.WriteLine("Output filename is required.");
    return;
}

var lines = File.ReadAllLines(filename);
if (lines.Length == 0)
{
    Console.WriteLine("File is empty.");
    return;
}

if(!bool.TryParse(ConfigurationManager.AppSettings["RandomChange"], out bool randomChange) ||
    !int.TryParse(ConfigurationManager.AppSettings["RandomDivisor"], out int randomDivisor))
{
    Console.WriteLine("Missing application settings.");
    return;
}

var settings= new CashRegisterSettings(randomChange, randomDivisor);

var cashRegister = new CashRegister(new USD(), settings);
var transactions = TransactionParser.File(filename);

using (StreamWriter outputFile = new StreamWriter(outputFilename))
{
    foreach (var (amountOwed, amountPaid) in transactions)
    {
        try
        {
            var change = cashRegister.Transaction(amountOwed, amountPaid);
            outputFile.WriteLine(CashRegister.GetChangeText(change));
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
    }     
}
