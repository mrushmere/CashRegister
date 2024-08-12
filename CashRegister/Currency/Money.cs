using System.Transactions;

namespace CashRegisterNS.Currency {
    public class Money {
        // type either bill or coin
        public decimal Amount { get; set; }

        public readonly ICurrency Currency;
        public Money(ICurrency currency, decimal amount) {
            if (amount < 0) {
                throw new System.ArgumentException("Amount cannot be negative");
            }
            Currency = currency;
            Amount = amount;

        }

    }
}