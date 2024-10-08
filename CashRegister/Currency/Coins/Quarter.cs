namespace CashRegisterNS.Currency.Coin {
    public class Quarter(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 0.25m;
        public override string Name => "Quarter";
        public override string PluralName => "Quarters";
    }
}