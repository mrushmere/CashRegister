namespace CashRegisterNS.Currency.Bill {
    public class Five(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 5m;
    }
}