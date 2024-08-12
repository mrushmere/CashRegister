namespace CashRegisterNS.Currency.Bill {
    public class Dollar(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 1m;
    }
}