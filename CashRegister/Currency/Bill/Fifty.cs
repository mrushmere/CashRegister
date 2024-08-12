namespace CashRegisterNS.Currency.Bill {
    public class Fifty(ICurrency currency) : Money(currency, amount) {
        private static readonly decimal amount = 50m;
    }
}