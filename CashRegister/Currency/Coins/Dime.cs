namespace CashRegisterNS.Currency.Coin {
    public class Dime(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 0.1m;
        public override string Name => "Dime";
        public override string PluralName => "Dimes";
    }
}