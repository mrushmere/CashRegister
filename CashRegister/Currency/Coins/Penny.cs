namespace CashRegisterNS.Currency.Coin {
    public class Penny(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 0.01m;

        public override string Name => "Penny";
        public override string PluralName => "Pennies";
    }
}