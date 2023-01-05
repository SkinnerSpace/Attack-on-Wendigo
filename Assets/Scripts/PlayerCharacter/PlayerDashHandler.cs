using UnityEngine;

public class PlayerDashHandler : MonoBehaviour
{
    private const float AIR_MULTIPLIER = 2f; 

    [SerializeField] private Camera cam;
    private Transform cameraTransform;
    private PlayerCharacter player;
    private PlayerHorizontalMovement horizontalMovement;
    private PlayerGroundDetector groundDetector;
    private FunctionTimer timer;

    [SerializeField] private float coolDownTime = 0.5f;
    [SerializeField] private float soaringTime = 0.1f;
    [SerializeField] private float dashDistance = 10f;
    private bool isCharged = true;
    public bool isSoaring { get; private set; }

    private void Awake()
    {
        cameraTransform = cam.transform;

        player = GetComponent<PlayerCharacter>();
        horizontalMovement = GetComponent<PlayerHorizontalMovement>();
        groundDetector = GetComponent<PlayerGroundDetector>();
        timer = GetComponent<FunctionTimer>();
    }

    public void HandleDash()
    {
        if (InputReader.dash && isCharged)
        {
            Dash();
        }
    }

    Vector3 startPos;

    private void Dash()
    {
        isCharged = false;
        isSoaring = true;

        Vector3 dashVelocity = Vector3.Scale(GetDirection(), GetPower());
        horizontalMovement.AddContinuousVelocity(dashVelocity);

        Debug.Log("Velocity " + dashVelocity.magnitude);

        timer.Set("Cool Down", coolDownTime, CoolDown);
        timer.Set("Stop Soaring", soaringTime, StopSoaring);

        startPos = transform.position;
        Debug.Log("START " + startPos);

        timer.Set("Measure", 2f, Measure);
    }

    private Vector3 GetDirection()
    {
        Vector3 forwardDirection = cameraTransform.forward;
        Vector3 flatDirection = new Vector3(forwardDirection.x, 0, forwardDirection.z).normalized;
        Debug.Log("Dir magn " + flatDirection.magnitude);

        return flatDirection;
    }

    private Vector3 GetPower()
    {
        float dragAdjustment = (player.Deceleration * Time.deltaTime) + 1f;
        float resistance = Mathf.Log(1f / dragAdjustment) / -Time.deltaTime;

        Vector3 dashPower = dashDistance * new Vector3(resistance, 0f, resistance);
        Debug.Log("Power " + dashPower.magnitude);

        return dashPower;
    }

    public void CoolDown()
    {
        isCharged = true;
        StopSoaring();
    }

    private void StopSoaring()
    {
        isSoaring = false;
    }

    private void Measure()
    {
        Vector3 currentPos = transform.position;
        Debug.Log("FINISH " + currentPos);
        Debug.Log("DISTANCE " + Vector3.Distance(startPos, currentPos));
    }
}
