namespace CashRegisterNS.Currency.Bill {
    public class Ten(ICurrency currency) : Money(currency, amount) {
        private static decimal amount = 10m;
        public override string Name => "Ten";
        public override string PluralName => "Tens";
    }
}