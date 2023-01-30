using UnityEngine;

public class SurfaceHitSFXPlayer : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference hitSFX;

    public float Threshold => threshold;
    [SerializeField] private float threshold;
    
    [Range(1,10)]
    [SerializeField] private int variety;
    [Range(0f,10f)]
    [SerializeField] private float pitchRange; 

    private AudioPlayer audioPlayer;

    private void Awake()
    {
        AddToManager();
        audioPlayer = AudioPlayer.Create(hitSFX).WithVariety(variety).WithPitch(-pitchRange, pitchRange);
    }

    private void AddToManager()
    {
        SurfaceSFXManager manager = GetComponent<SurfaceSFXManager>();
        manager.AddSFXPlayer(this);
    }

    public void SetPosition(Vector3 position) => audioPlayer.WithPosition(position); 

    public void ShiftPitch(float shift)
    {
        float min = -pitchRange + shift;
        float max = pitchRange + shift;
        audioPlayer.WithPitch(min, max);
    }

    public void Play()
    {
        audioPlayer.PlayOneShot();
    }
}