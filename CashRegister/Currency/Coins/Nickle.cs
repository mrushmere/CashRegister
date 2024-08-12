namespace CashRegisterNS.Currency.Coin {
    public class Nickel(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 0.05m;
    }
}