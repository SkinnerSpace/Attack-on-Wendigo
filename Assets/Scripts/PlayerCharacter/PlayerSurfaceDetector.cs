using UnityEngine;

public class PlayerSurfaceDetector : MonoBehaviour
{
    private const int GROUND = 1 << 8;
    private const float CHECK_HEIGHT = 1f;

    private PlayerCharacter player;

    private Vector3 surfaceCheckOffset;
    public ISurface surface { get; private set; }
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
        Vector3 checkPoint = transform.position - surfaceCheckOffset;
        Ray ray = new Ray(checkPoint, Vector3.down * CHECK_HEIGHT);

        if (Physics.Raycast(ray, out RaycastHit hit, GROUND))
        {
            surface = hit.transform.GetComponent<ISurface>();
            position = hit.point;
        }
        else
        {
            surface = null;
            NULLSURFACE
        }
    }
}
