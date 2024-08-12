using System.Transactions;

namespace CashRegisterNS.Currency {
    public abstract class Money {
        public decimal Amount { get; set; }
        public abstract string Name { get; }
        public abstract string PluralName { get; }

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