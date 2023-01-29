using UnityEngine;

public class PlayerHorizontalMover : MonoBehaviour, IPushable
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PlayerDashHandler dashHandler;
    [SerializeField] private Speedometer speedometer;

    [SerializeField] private Transform cam;

    public Vector3 velocity { get; private set; }
    private Vector3 forceVelocity;
    private Vector3 movementVelocity;
    private float resistance = 1f;

    public float velocityFOVModifier { get; private set; }
    public float velocityMagnitude { get; private set; }

    public Vector3 position => player.position;
    public Vector3 direction => movementVelocity.normalized;

    private void Start()
    {
        Blizzard.Instance.AddPushable(this);
    }

    private void Update() => HandleArbitraryMovement();

    private void FixedUpdate()
    {
        if (InputReader.dash) dashHandler.Dash();

        velocity = (movementVelocity + forceVelocity) * resistance;
        forceVelocity = DecelerateForce();

        player.horizontalVelocity = velocity;
        characterController.Move(velocity * Time.deltaTime);

        NotifyOnChange();
    }

    private void HandleArbitraryMovement()
    {
        Vector3 direction = GetDirection();
        player.Speed = PlayerIsMoving(direction) ? AccelerateSpeed() : player.MinSpeed;
        movementVelocity = direction * player.Speed;
    }

    private Vector3 GetDirection() => (InputReader.normDir.x * transform.right) + (InputReader.normDir.z * transform.forward);

    private bool PlayerIsMoving(Vector3 direction) => direction.magnitude > 0f;

    private float AccelerateSpeed() => Mathf.Lerp(player.Speed, player.MaxSpeed, player.Acceleration * Time.deltaTime);

    private Vector3 DecelerateForce() => Vector3.Lerp(forceVelocity, Vector3.zero, player.Deceleration * Time.deltaTime);

    private void NotifyOnChange()
    {
        speedometer.MeasureSpeed(velocity.magnitude, player.MaxSpeed);

        float velocitySpeedDifference = Mathf.Max((velocity.magnitude - player.MinSpeed), 0f);
        velocityFOVModifier = velocitySpeedDifference / (player.MaxSpeed - player.MinSpeed);
    }

    private float GetDirectedness()
    {
        Vector2 camDir = new Vector2(cam.forward.x, cam.forward.z).normalized;
        Vector2 flatVelocity = new Vector2(velocity.x, velocity.z).normalized;
        float directedness = Vector2.Dot(camDir, flatVelocity);

        return directedness;
    }

    public void ApplyForce(Vector3 force) => AddForce(force);
    public void AddForce(Vector3 extraVelocity) => forceVelocity += extraVelocity;
    public void SetResistance(float resistance) => this.resistance = resistance;
}
