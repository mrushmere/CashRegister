using CashRegisterNS.Currency.Bill;
using CashRegisterNS.Currency.Coin;

namespace CashRegisterNS.Currency {
    public class USD : ICurrency {

        
        public USD() {
            IsoCode = "USD";
            Symbol = '$';
        }

        public string IsoCode { get; } 
        public char Symbol { get;}

        public List<Money> GetDenominations() => [
            new Hundred(this),
            new Fifty(this),
            new Twenty(this),
            new Ten(this),
            new Five(this),
            new Dollar(this),
            new Quarter(this),
            new Dime(this),
            new Nickel(this),
            new Penny(this)
        ];
    }


}