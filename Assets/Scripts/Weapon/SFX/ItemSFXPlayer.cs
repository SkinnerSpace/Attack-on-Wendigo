using UnityEngine;

public class ItemSFXPlayer : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference takeSFX;
    [SerializeField] private FMODUnity.EventReference dropSFX;
    [SerializeField] private FMODUnity.EventReference throwSFX;

    private AudioPlayer takePlayer;
    private AudioPlayer dropPlayer;
    private AudioPlayer throwPlayer;

    private void Awake()
    {
        takePlayer = AudioPlayer.Create(takeSFX).WithPitch(-2f, 2f);
        dropPlayer = AudioPlayer.Create(dropSFX).WithPitch(-0.5f, 0.5f);
        throwPlayer = AudioPlayer.Create(throwSFX).WithPitch(-2f, 0f);
    }

    public void PlayTakeSFX() => takePlayer.PlayOneShot();
    public void PlayDropSFX(){
        //dropPlayer.PlayOneShot();
        throwPlayer.PlayOneShot();
    }
}
