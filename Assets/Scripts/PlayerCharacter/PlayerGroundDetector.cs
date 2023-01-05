using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    public static float landMagnitude { get; private set; }

    private const int GROUND = 1 << 8;
    private const float GROUND_CHECK_RADIUS = 0.1f;

    private Vector3 groundCheckOffset;
    public bool isGrounded { get; private set; }

    private PlayerCharacter player;
    private PlayerSurfaceDetector surfaceDetector;
    private PlayerDashHandler dashHandler;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        surfaceDetector = GetComponent<PlayerSurfaceDetector>();
        dashHandler = GetComponent<PlayerDashHandler>();
    }

    private void Start()
    {
        float offset = (player.height / 2f) + GROUND_CHECK_RADIUS;
        groundCheckOffset = new Vector3(0f, offset, 0f);
    }

    public void UpdateDetection()
    {
        bool wasGrounded = isGrounded;
        isGrounded = CheckIsGrounded();
        Landing(wasGrounded);
    }

    private bool CheckIsGrounded()
    {
        Vector3 checkPoint = transform.position - groundCheckOffset;
        return Physics.CheckSphere(checkPoint, GROUND_CHECK_RADIUS, GROUND);
    }

    private void Landing(bool wasGrounded)
    {
        if ((wasGrounded != isGrounded) && isGrounded)
        {
            landMagnitude = 1f;
            surfaceDetector.UpdateSurfaceData();
            dashHandler.CoolDown();
        }

        landMagnitude = Mathf.Lerp(landMagnitude, 0f, Time.deltaTime);
    }
}
