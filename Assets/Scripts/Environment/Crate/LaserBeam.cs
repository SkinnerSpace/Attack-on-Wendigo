using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float emissionHeight = 1000f;
    [SerializeField] private LaserBeamFader fader;
    [SerializeField] private CrateSFXPlayer sFXPlayer;

    private LineRenderer line;
    private UpDirectionFinder upDirFinder;

    private bool isActive;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        upDirFinder = new UpDirectionFinder();
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
        /*        Vector3 upDir = upDirFinder.GetUpwardDirection(transform);
                Vector3 upPos = transform.position + (upDir * EMISSION_HEIGHT);

                line.SetPosition(0, transform.position);
                line.SetPosition(1, upPos);*/

        isActive = true;
        line.enabled = true;

        fader.FadeIn();
        sFXPlayer.PlayLightOn();
    }

    public void ResetLaserBeam()
    {
        fader.ResetFade();
        line.enabled = false;
    }

    public void SwitchOff() => fader.FadeOut();
}
