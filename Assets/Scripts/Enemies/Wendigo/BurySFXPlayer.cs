using UnityEngine;

public class BurySFXPlayer : MonoBehaviour
{
    private const float BLOOD_GURGLE_TIME = 5.5f;
    private const float BLOOD_STREAM_TIME = 9f;

    [Header("Required Components")]
    [SerializeField] private FunctionTimer timer;

    [Header("Audio References")]
    [SerializeField] private FMODUnity.EventReference burySFX;
    [SerializeField] private FMODUnity.EventReference bloodGurgleSFX;
    [SerializeField] private FMODUnity.EventReference bloodStreamSFX;

    private AudioPlayer buryPlayer;
    private AudioPlayer bloodGurglePlayer;
    private AudioPlayer bloodStreamPlayer;

    private void Awake()
    {
        buryPlayer = AudioPlayer.Create(burySFX).WithPitch(-1f, 1f);
        bloodGurglePlayer = AudioPlayer.Create(bloodGurgleSFX).WithPitch(-3f, 0f);
        bloodStreamPlayer = AudioPlayer.Create(bloodStreamSFX).WithPitch(-3f, 0f);
    }

    public void PlayBurySFX()
    {
        buryPlayer.WithPosition(transform.position).PlayOneShot();
        bloodGurglePlayer.WithPosition(transform.position).WithRandomStartTime().PlayLoop();
        bloodStreamPlayer.WithPosition(transform.position).WithRandomStartTime().PlayLoop();

        timer.Set("StopGurgle", BLOOD_GURGLE_TIME, () => bloodGurglePlayer.Stop());
        timer.Set("StopStream", BLOOD_STREAM_TIME, () => bloodStreamPlayer.Stop());
        Debug.Log("Playt");
    }
}

