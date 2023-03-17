using UnityEngine;

public interface IShakeBuilder
{
    IShakeBuilder withTime(float time);
    IShakeBuilder WithAxis(float x, float y, float z);
    IShakeBuilder WithStrength(float amount, float angleMultiplier);
    IShakeBuilder WithCurve(float frequency, float attack, float release);
    IShakeBuilder WithAttenuation(Vector3 position, Transform subject, float maxDistance);
    void BuildAndLaunch(IShakeManager shakeManager);
    IShake Build();
}