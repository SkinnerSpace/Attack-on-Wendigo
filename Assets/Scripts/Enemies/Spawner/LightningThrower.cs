using UnityEngine;

public class LightningThrower : MonoBehaviour
{
    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private FMODUnity.EventReference thunderSFX;
    private AudioPlayer thunderSFXPlayer;

    private void Awake()
    {
        thunderSFXPlayer = AudioPlayer.Create(thunderSFX).WithPitch(-4f, 4f);
    }

    public void Throw(Vector3 position)
    {
        Instantiate(lightningBolt, position, Quaternion.identity, transform);

        float dist = Vector3.Distance(position, PlayerCharacter.Instance.transform.position);
        ScreenShake.Create().withTime(1f).WithAxis(1f, 1f, 0f).WithStrength(1f, 4f).WithCurve(10f, 0.1f, 0.25f).WithAttenuation(dist, 600f).Launch();
        thunderSFXPlayer.WithPosition(position).PlayOneShot();
    }
}
