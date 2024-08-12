namespace CashRegisterNS.Currency.Bill {
    public class Hundred(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 100m;
    }
}