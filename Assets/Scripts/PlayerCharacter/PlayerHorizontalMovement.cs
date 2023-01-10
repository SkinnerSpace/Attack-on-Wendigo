using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    private const float MAX_SPEED_VELOCITY_RELATION = 3f;
    public static float velocityMagnitude { get; private set; }

    private PlayerCharacter player;
    private CharacterController controller;
    private PlayerDashHandler dashHandler;

    private Vector3 continuousVelocity;
    private Vector3 discreteVelocity;
    public Vector3 velocity { get; private set; }
    public float speedMagnitude { get; private set; }

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
        dashHandler = GetComponent<PlayerDashHandler>();
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

    private Vector3 GetDirection()
    {
        Vector3 direction = (InputReader.normDir.x * transform.right) + (InputReader.normDir.z * transform.forward);
        return direction;
    }

    private bool PlayerIsMoving(Vector3 direction)
    {
        return direction.magnitude > 0f;
    }

    private float AccelerateSpeed()
    {
        return Mathf.Lerp(player.Speed, player.MaxSpeed, player.Acceleration * Time.deltaTime);
    }

    private void HandleContinuousVelocity()
    {
        if (InputReader.dash) dashHandler.Dash();

        velocity = discreteVelocity + continuousVelocity;
        continuousVelocity = Vector3.Lerp(continuousVelocity, Vector3.zero, player.Deceleration * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        controller.Move(velocity * Time.deltaTime);

        velocityMagnitude = velocity.magnitude / player.MaxSpeed;

        float speedVelocityRelation = Mathf.Max((velocity.magnitude - player.MinSpeed), 0f);
        speedMagnitude = speedVelocityRelation / (player.MaxSpeed - player.MinSpeed);
    }

    public void AddContinuousVelocity(Vector3 extraVelocity)
    {
        continuousVelocity += extraVelocity;
    }
}
