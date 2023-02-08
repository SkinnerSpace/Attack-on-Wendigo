using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private const float EMISSION_HEIGHT = 100f;

    private LineRenderer line;
    private UpDirFinder upDirFinder;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        upDirFinder = GetComponent<UpDirFinder>();
    }

    public void SwitchOn()
    {
        Vector3 upDir = upDirFinder.GetUpwardDirection();
        Vector3 upPos = transform.position + (upDir * EMISSION_HEIGHT);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, upPos);
        line.enabled = true;
    }

    public void SwitchOff() => line.enabled = false;
}
