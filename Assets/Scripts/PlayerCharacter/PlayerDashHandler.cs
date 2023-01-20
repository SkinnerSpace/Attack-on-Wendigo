using UnityEngine;

public class PlayerDashHandler : MonoBehaviour
{
    private const float DOWN_MULTIPLIER = 2f;
    private const float COOL_DOWN_TIME = 0.5f;
    private const float SOARING_TIME = 0.1f;

    [SerializeField] private PlayerLook look;

    private PlayerCharacter player;
    private PlayerHorizontalMovement horizontalMovement;
    private FunctionTimer timer;

    private bool isCharged = true;
    public bool isSoaring { get; private set; }

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        horizontalMovement = GetComponent<PlayerHorizontalMovement>();
        timer = GetComponent<FunctionTimer>();
    }

    public void Dash()
    {
        if (isCharged)
        {
            isCharged = false;
            isSoaring = true;

            Vector3 direction = look.IsLookingDown() ? (look.transform.forward) : GetFlatDirection();
            Vector3 resistanceDirection = look.IsLookingDown() ? (new Vector3(0, DOWN_MULTIPLIER, 0)) : (new Vector3(1, 0, 1));
            Vector3 dashVelocity = Vector3.Scale(direction, GetPower(resistanceDirection));

            horizontalMovement.AddForce(dashVelocity);

            timer.Set("Cool Down", COOL_DOWN_TIME, CoolDown);
            timer.Set("Stop Soaring", SOARING_TIME, StopSoaring);
        }
    }

    private Vector3 GetFlatDirection()
    {
        Vector3 direction = GetDirection();
        Vector3 flatDirection = new Vector3(direction.x, 0, direction.z).normalized;

        return flatDirection;
    }

    private Vector3 GetDirection()
    {
        if (InputReader.forward) return look.transform.forward;
        if (InputReader.backward) return look.transform.forward * -1f;

        if (InputReader.right) return look.transform.right;
        if (InputReader.left) return look.transform.right * -1f;

        return look.transform.forward;
    }

    private Vector3 GetPower(Vector3 resistanceDirection)
    {
        float dragAdjustment = (player.Deceleration * Time.deltaTime) + 1f;
        float resistance = Mathf.Log(1f / dragAdjustment) / -Time.deltaTime;
        Vector3 fulllResistance = resistance * resistanceDirection;

        float distanceAdjustment = player.Deceleration * 0.1f;

        Vector3 dashPower = (player.DashDistance + distanceAdjustment) * fulllResistance;

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
}
