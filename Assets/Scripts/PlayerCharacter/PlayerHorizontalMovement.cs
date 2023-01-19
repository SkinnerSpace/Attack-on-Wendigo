using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour, IPushable
{
    private const float MAX_SPEED_VELOCITY_RELATION = 3f;
    public static float velocityMagnitude { get; private set; }

    private PlayerCharacter player;
    private CharacterController controller;
    private PlayerDashHandler dashHandler;

    private Vector3 continuousVelocity;
    private Vector3 discreteVelocity;
    public Vector3 velocity { get; private set; }
    public float velocityFOVModifier { get; private set; }


    public Vector3 position => player.position;
    public Vector3 direction => discreteVelocity.normalized;
    private float resistance = 1f;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
        dashHandler = GetComponent<PlayerDashHandler>();
    }

    private void Start()
    {
        Blizzard.Instance.AddPushable(this);
    }

    private void Update()
    {
        HandleDiscreteVelocity();
    }

    private void FixedUpdate()
    {
        HandleContinuousVelocity();
        ApplyMovement();
    }

    private void HandleDiscreteVelocity()
    {
        Vector3 direction = GetDirection();
        player.Speed = PlayerIsMoving(direction) ? AccelerateSpeed() : player.MinSpeed;
        discreteVelocity = direction * player.Speed;
    }

    private Vector3 GetDirection() => (InputReader.normDir.x * transform.right) + (InputReader.normDir.z * transform.forward);

    private bool PlayerIsMoving(Vector3 direction) => direction.magnitude > 0f;

    private float AccelerateSpeed() => Mathf.Lerp(player.Speed, player.MaxSpeed, player.Acceleration * Time.deltaTime);

    private void HandleContinuousVelocity()
    {
        if (InputReader.dash) dashHandler.Dash();

        velocity = (discreteVelocity + continuousVelocity) * resistance;
        continuousVelocity = Vector3.Lerp(continuousVelocity, Vector3.zero, player.Deceleration * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        controller.Move(velocity * Time.deltaTime);

        velocityMagnitude = velocity.magnitude / player.MaxSpeed;

        float speedVelocityRelation = Mathf.Max((velocity.magnitude - player.MinSpeed), 0f);

        WARNING
        // Find dor product betweeen velocity and the direction of the camera
        velocityFOVModifier = speedVelocityRelation / (player.MaxSpeed - player.MinSpeed);
    }

    public void AddContinuousVelocity(Vector3 extraVelocity) => continuousVelocity += extraVelocity;

    public void SetResistance(float resistance) => this.resistance = resistance;
    public void ApplyForce(Vector3 force) => AddContinuousVelocity(force);
}
