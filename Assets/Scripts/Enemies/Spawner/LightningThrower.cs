using UnityEngine;

public class LightningThrower : MonoBehaviour
{
    private const float MAX_DISTANCE = 600f;

    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private FMODUnity.EventReference thunderSFX;

    [SerializeField] private Transform attenuationPoint;
    private AudioPlayer thunderSFXPlayer;

    private IObjectPooler pooler;

    private void Awake()
    {
        thunderSFXPlayer = AudioPlayer.Create(thunderSFX).WithPitch(-4f, 4f);
    }

    private void Start(){
        pooler = PoolHolder.Instance;
    }

    public void Throw(Vector3 position)
    {
        pooler.SpawnFromThePool("Lightning", position, Quaternion.identity);

        ShakeBuilder.Create().
                        withTime(1f).
                        WithAxis(1f, 1f, 0f).
                        WithStrength(1f, 4f).
                        WithCurve(10f, 0.1f, 0.25f).
                        WithAttenuation(position, attenuationPoint, MAX_DISTANCE).
                        BuildAndLaunch(ShakeManagerComponent.Instance);

        thunderSFXPlayer.WithPosition(position).PlayOneShot();
    }
}
