public struct ShakeCurve
{
    public float frequency { get; private set; }
    public float attack { get; private set; }
    public float release { get; private set; }
    
    public ShakeCurve(float frequency, float attack, float release)
    {
        this.frequency = frequency;
        this.attack = attack;
        this.release = release;
    }
}

