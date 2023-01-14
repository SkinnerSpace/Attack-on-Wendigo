public struct ShakeStrength
{
    public float amount { get; private set; }
    public float angleMultiplier { get; private set; }

    public ShakeStrength(float amount, float angleMultiplier)
    {
        this.amount = amount;
        this.angleMultiplier = angleMultiplier;
    }
}

