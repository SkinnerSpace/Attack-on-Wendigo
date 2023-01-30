using UnityEngine;

public class ParticleCountManager
{
    private short minCount;
    private short maxCount;

    public void SetCount(short minCount, short maxCount)
    {
        this.minCount = minCount;
        this.maxCount = maxCount;
    }

    public void AdjustCount(float tangent)
    {
        float reduction = Mathf.Lerp(0.5f, 1f, tangent);
        minCount = (short)(minCount * reduction);
        maxCount = (short)(maxCount * reduction);
    }

    public void ApplyCount(ParticleSystem particle)
    {
        ParticleSystem.EmissionModule emission = particle.emission;
        ParticleSystem.Burst burst = new ParticleSystem.Burst();

        burst.minCount = minCount;
        burst.maxCount = maxCount;

        emission.SetBurst(0, burst);
    }
}
