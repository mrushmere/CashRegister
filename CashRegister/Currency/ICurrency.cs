namespace CashRegisterNS.Currency {
    public interface ICurrency
    {
        string IsoCode { get; } 

        char Symbol { get; }

         List<Money> GetDenominations();
    }

}