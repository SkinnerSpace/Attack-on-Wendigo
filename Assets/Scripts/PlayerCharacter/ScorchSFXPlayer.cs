using UnityEngine;

public class ScorchSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference inflameSFX;
    [SerializeField] private FMODUnity.EventReference damageSFX;
    [SerializeField] private FMODUnity.EventReference coolDownSFX;

    private AudioPlayer inflamePlayer;
    private AudioPlayer damagePlayer;
    private AudioPlayer coolDownPlayer;

    private void Awake()
    {
        inflamePlayer = AudioPlayer.Create(inflameSFX).WithVariety(3).WithPitch(-1f, 1f);
        damagePlayer = AudioPlayer.Create(damageSFX).WithVariety(3).WithPitch(-1f, 1f);
        coolDownPlayer = AudioPlayer.Create(coolDownSFX).WithVariety(3).WithPitch(-1f, 1f);
    }

    public void PlayInflameSFX() => inflamePlayer.PlayOneShot();
    public void PlayDamageSFX() => damagePlayer.PlayOneShot();
    public void PlayCoolDownSFX() => coolDownPlayer.PlayOneShot();
}