using UnityEngine;

public class WeaponSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference shootSFX;
    [SerializeField] private FMODUnity.EventReference isEmptySFX;

    private AudioPlayer shootPlayer;
    private AudioPlayer isEmptyPlayer;

    private void Awake()
    {
        shootPlayer = AudioPlayer.Create(shootSFX).WithPitch(-2f, 2f);
        isEmptyPlayer = AudioPlayer.Create(isEmptySFX).WithPitch(-0.5f, 0.5f);
    }

    public void PlayShootSFX() => shootPlayer.PlayOneShot();
    public void PlayIsEmptySFX() => isEmptyPlayer.PlayOneShot();
}
