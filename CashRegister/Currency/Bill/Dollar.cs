namespace CashRegisterNS.Currency.Bill {
    public class Dollar(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 1m;
        public override string Name => "Dollar";
        public override string PluralName => "Dollars";
    }
}