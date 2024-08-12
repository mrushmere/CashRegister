namespace CashRegisterNS
{
    public interface ICashRegisterSettings
    {
        bool RandomChange { get; }
        int? RandomDivisor { get; }
    }
}