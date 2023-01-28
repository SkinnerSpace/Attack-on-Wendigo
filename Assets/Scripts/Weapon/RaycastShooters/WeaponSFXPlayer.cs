using UnityEngine;

public class WeaponSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference shootSFX;

    private AudioPlayer shootPlayer;

    private void Awake()
    {
        shootPlayer = AudioPlayer.Create(shootSFX).WithPitch(-2, 2f);
    }

    public void PlayShootSFX() => shootPlayer.PlayOneShot();
}
