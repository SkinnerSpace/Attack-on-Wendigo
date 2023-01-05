using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    public static float velocityMagnitude { get; private set; }
    public static float speedMagnitude { get; private set; }

    private PlayerCharacter player;
    private CharacterController controller;
    private PlayerDashHandler dashHandler;

    private Vector3 continuousVelocity;
    private Vector3 discreteVelocity;
    public Vector3 velocity { get; private set; }
    
    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
        dashHandler = GetComponent<PlayerDashHandler>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void HandleMovement()
    {
        

        
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

    private void ApplyMovement()
    {
        Vector3 direction = GetDirection();
        player.Speed = PlayerIsMoving(direction) ? AccelerateSpeed() : player.MinSpeed;
        discreteVelocity = direction * player.Speed;

        dashHandler.HandleDash();

        velocity = discreteVelocity + continuousVelocity;
        continuousVelocity = Vector3.Lerp(continuousVelocity, Vector3.zero, player.Deceleration * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);

        velocityMagnitude = velocity.magnitude / player.MaxSpeed;
        speedMagnitude = Mathf.Max((velocity.magnitude - player.MinSpeed), 0f) / (player.MaxSpeed - player.MinSpeed);
    }

    public void AddContinuousVelocity(Vector3 extraVelocity)
    {
        continuousVelocity += extraVelocity;
        Debug.Log("EXTRA " + extraVelocity.magnitude);
    }
}
