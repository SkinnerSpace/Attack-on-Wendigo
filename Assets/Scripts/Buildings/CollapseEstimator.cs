using UnityEngine;

public static class CollapseEstimator
{
    private const float TIME_PER_METER = 0.2f;
    private const float FREQUENCY_PER_SECOND = 10f;

    public static CollapseEstimations EstimateFor(float height)
    {
        float time = height * TIME_PER_METER;
        float frequency = time * FREQUENCY_PER_SECOND;

        return new CollapseEstimations(time, frequency);
    }
}

