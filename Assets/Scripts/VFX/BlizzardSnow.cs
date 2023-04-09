using UnityEngine;

public class BlizzardSnow : MonoBehaviour
{
    [SerializeField] private Blizzard blizzard;
    [SerializeField] private int maxEmissionRate = 1000;
    [SerializeField] private int maxRateOverDistance = 100;

    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        var emission = particles.emission;
        emission.rateOverTime = maxEmissionRate * blizzard.Influence;
        emission.rateOverDistance = maxRateOverDistance * blizzard.Influence;
    }
}
