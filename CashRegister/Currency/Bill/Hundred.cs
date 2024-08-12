namespace CashRegisterNS.Currency.Bill {
    public class Hundred(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 100m;
        public override string Name => "Hundred";
        public override string PluralName => "Hundreds";
    }
}