using UnityEngine;

public class PlayerSurfaceDetector : MonoBehaviour
{
    private const int GROUND = 1 << 8;
    private const float CHECK_HEIGHT = 1f;

    private PlayerCharacter player;

    private Vector3 surfaceCheckOffset;
    public Surface surface { get; private set; }
    public Vector3 position { get; private set; }

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
    }

    private void Start()
    {
        float offset = (player.height / 2f);
        surfaceCheckOffset = new Vector3(0f, offset, 0f);
    }

    public void UpdateSurfaceData()
    {
        SurfaceProbe probe = ProbeTheSurface();
        position = probe.position;

        surface = (probe.surface != null) ? probe.surface : null;
    }

    private SurfaceProbe ProbeTheSurface()
    {
        Vector3 checkPoint = transform.position - surfaceCheckOffset;
        Ray ray = new Ray(checkPoint, Vector3.down * CHECK_HEIGHT);
        SurfaceProbe probe = new SurfaceProbe();

        if (Physics.Raycast(ray, out RaycastHit hit, GROUND))
        {
            probe.surface = hit.transform.GetComponent<Surface>();
            probe.position = hit.point;
        }

        return probe;
    }
}
