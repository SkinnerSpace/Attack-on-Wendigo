using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    public static float landMagnitude { get; private set; }

    private float groundCheckRadius => characterController.radius;

    private Vector3 groundCheckOffset;
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float detectionRadius;
    [SerializeField] private float detectionHeight;

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
        float offset = (player.height / 2f) + groundCheckRadius;
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
        return Physics.CheckSphere(GetCheckPos(), detectionRadius, ComplexLayers.Solid);
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

    private Vector3 GetCheckPos() => transform.position - new Vector3(0f, detectionHeight, 0f);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetCheckPos(), detectionRadius);
    }
}
