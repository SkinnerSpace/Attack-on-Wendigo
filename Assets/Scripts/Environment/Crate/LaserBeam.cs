using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float emissionHeight = 1000f;
    [SerializeField] private LaserBeamFader fader;
    [SerializeField] private CrateSFXPlayer sFXPlayer;

    private LineRenderer line;

    private bool isActive;
    private bool isLocked;

    private void Awake(){
        line = GetComponent<LineRenderer>();
    }

    private void Start(){
        GameEvents.current.onVictory += SwitchOffAndLock;
    }

    private void Update()
    {
        if (isActive){
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + (Vector3.up * emissionHeight));
        }
    }

    public void SwitchOn()
    {
        if (!isLocked)
        {
            isActive = true;
            line.enabled = true;

            fader.FadeIn();
            sFXPlayer.PlayLightOn();
        }
    }

    public void ResetLaserBeam()
    {
        fader.ResetFade();
        isLocked = false;
        line.enabled = false;
    }

    public void SwitchOffAndLock()
    {
        isLocked = true;
        SwitchOff();
    }

    public void SwitchOff() => fader.FadeOut();
}
