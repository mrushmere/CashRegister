namespace CashRegisterNS.Currency.Bill {
    public class Ten(ICurrency currency) : Money(currency, amount) {
        private static decimal amount = 10m;
    }
}