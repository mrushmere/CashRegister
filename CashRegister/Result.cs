using CashRegisterNS.Currency;

namespace CashRegisterNS
{
    public class Result
    { 
        public string? Error { get; set; }
        public Dictionary<Money, int> Value { get; set; } = [];
    }
}
