using UnityEngine;

public class ItemSFXPlayer : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Pickable pickable;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference takeSFX;
    [SerializeField] private FMODUnity.EventReference throwSFX;

    private AudioPlayer takePlayer;
    private AudioPlayer throwPlayer;

    private void Awake()
    {
        pickable.onPickedUp += PlayTakeSFX;
        pickable.onDropped += PlayThrowSFX;

        takePlayer = AudioPlayer.Create(takeSFX).WithPitch(-2f, 2f);
        throwPlayer = AudioPlayer.Create(throwSFX).WithPitch(-2f, 0f);
    }

    public void PlayTakeSFX() => takePlayer.PlayOneShot();
    public void PlayThrowSFX() => throwPlayer.PlayOneShot();
}
