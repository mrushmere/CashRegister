using CashRegisterNS;

public class CashRegisterSettings : ICashRegisterSettings
{
    public readonly bool randomChange;
    public readonly int? randomDivisor;

    public CashRegisterSettings(bool randomChange, int? randomDivisor)
    {
        this.randomChange = randomChange;

        if( randomDivisor > 0)
        {
            this.randomDivisor = randomDivisor;
        }
    }

    public bool RandomChange => randomChange;

    public int? RandomDivisor => randomDivisor;
}