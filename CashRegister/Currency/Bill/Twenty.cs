namespace CashRegisterNS.Currency.Bill {
    public class Twenty(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 20m;
        public override string Name => "Twenty";
        public override string PluralName => "Twenties";
    }
}