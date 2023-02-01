using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    public static float landMagnitude { get; private set; }

    private const int GROUND = 1 << 8;
    private const float GROUND_CHECK_RADIUS = 0.1f;

    private Vector3 groundCheckOffset;

    private bool wasGrounded;
    public bool isGrounded { get; private set; }

    private PlayerCharacter player;
    private PlayerSurfaceDetector surfaceDetector;
    private PlayerDashHandler dashHandler;

    public static PlayerGroundDetector Instance { get; private set; }

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        surfaceDetector = GetComponent<PlayerSurfaceDetector>();
        dashHandler = GetComponent<PlayerDashHandler>();

        Instance = this;
    }

    private void Start()
    {
        float offset = (player.height / 2f) + GROUND_CHECK_RADIUS;
        groundCheckOffset = new Vector3(0f, offset, 0f);
    }

    public void UpdateDetection()
    {
        wasGrounded = isGrounded;
        isGrounded = CheckIsGrounded();
        Landing();
    }

    private bool CheckIsGrounded()
    {
        Vector3 checkPoint = transform.position - groundCheckOffset;
        return Physics.CheckSphere(checkPoint, GROUND_CHECK_RADIUS, ComplexLayers.Solid);
    }

    private void Landing()
    {
        if (HasGrounded()) Land();

        landMagnitude = Mathf.Lerp(landMagnitude, 0f, Time.deltaTime);
    }

    private bool HasGrounded() => (wasGrounded != isGrounded) && isGrounded;

    private void Land()
    {
        landMagnitude = 1f;
        surfaceDetector.UpdateSurfaceData();
        HitTheSurface();
        dashHandler.CoolDown();
    }

    private void HitTheSurface()
    {
        if (surfaceDetector.surface != null)
        {
            Surface surface = surfaceDetector.surface;
            Vector3 direction = player.velocity.normalized;

            surface.Hit().
                    WithPosition(surfaceDetector.position).
                    WithAngle(direction, Vector3.up).
                    WithShape(1f, 70f).
                    WithCount(5, 10).
                    Launch();
        }
    }
}
