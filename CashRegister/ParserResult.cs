namespace CashRegisterNS
{
    public class ParserResult
    {
        public string? Error { get; set; }
        public List<(decimal amountOwed, decimal amountPaid)> Value { get; set; } = [];
    }
}
