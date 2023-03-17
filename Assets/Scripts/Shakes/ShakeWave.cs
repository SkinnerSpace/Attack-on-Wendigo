using UnityEngine;

public class ShakeWave
{
    private const float WAVE_UNIT = Mathf.PI * 2f;

    public float Value { get; private set; }
    public float PreviousRaw;
    public float CurrentRaw;

    public ShakeCurve curve { get; private set; }
    public float completeness { get; private set; }

    public ShakeWave(ShakeCurve curve) => this.curve = curve;

    public void Update(float completeness)
    {
        this.completeness = completeness;

        PreviousRaw = CurrentRaw;
        CurrentRaw = GetRaw(completeness);

        Value = GetAttuned(CurrentRaw, completeness);
    }


    public float GetRaw(float completeness) => Mathf.Sin(completeness * WAVE_UNIT * curve.frequency);
    private float GetAttuned(float raw, float completeness) => raw * Amplitude.Calculate(completeness, curve.attack, curve.release);
    public bool HasPassed() => (PreviousRaw < 0f) && (CurrentRaw >= 0f);
}