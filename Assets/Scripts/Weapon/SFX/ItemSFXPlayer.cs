using UnityEngine;

public class ItemSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference takeSFX;

    private AudioPlayer takePlayer;

    private void Awake()
    {
        takePlayer = AudioPlayer.Create(takeSFX).WithPitch(-2f, 2f);
    }

    public void PlayTakeSFX() => takePlayer.PlayOneShot();
}
