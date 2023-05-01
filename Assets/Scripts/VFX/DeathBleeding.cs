using UnityEngine;

public class DeathBleeding : MonoBehaviour
{
    [SerializeField] private float bleedingTime = 3f;

    private ParticleSystem bleeding;
    private FunctionTimer timer;

    private void Awake()
    {
        bleeding = GetComponent<ParticleSystem>();
        timer = GetComponent<FunctionTimer>();
    }

    private void Start()
    {
        PlayerEvents.current.onHammered += Bleed;
    }

    private void Bleed()
    {
        bleeding.Play();
        ShakeTheEarth();
        timer.Set("StopBleeding", bleedingTime, StopBleeding);
    }

    public void ShakeTheEarth()
    {
        ShakeBuilder.Create().
            withTime(0.8f).
            WithAxis(1f, 1f, 0f).
            WithStrength(0.12f, 1.7f).
            WithCurve(16f, 0.1f, 0.5f).
            BuildAndLaunch(ShakeManagerComponent.Instance);
    }

    private void StopBleeding() => bleeding.Stop();
}