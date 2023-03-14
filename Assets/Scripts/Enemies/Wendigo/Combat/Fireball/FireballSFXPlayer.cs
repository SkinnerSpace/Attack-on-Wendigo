using UnityEngine;

public class FireballSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference throwSFX;
    [SerializeField] private FMODUnity.EventReference flySFX;
    [SerializeField] private FMODUnity.EventReference explosionSFX;

    private AudioPlayer throwAudioPlayer;
    private AudioPlayer flyAudioPlayer;
    private AudioPlayer explosionAudioPlayer;

    private void Awake()
    {
        throwAudioPlayer = AudioPlayer.Create(throwSFX).WithPitch(-2f, 2f);
        flyAudioPlayer = AudioPlayer.Create(flySFX).WithAnchor(transform);
        explosionAudioPlayer = AudioPlayer.Create(explosionSFX).WithPitch(-2f, 2f);
    }

    public void PlayThrowSFX(){
        throwAudioPlayer.WithPosition(transform.position).PlayOneShot();
    }

    public void PlayFlySFX(){
        flyAudioPlayer.PlayLoop();
    }

    public void StopFlySFX(){
        flyAudioPlayer.Stop();
    }

    public void PlayExplosionSFX(){
        explosionAudioPlayer.WithPosition(transform.position).PlayOneShot();
    }
}