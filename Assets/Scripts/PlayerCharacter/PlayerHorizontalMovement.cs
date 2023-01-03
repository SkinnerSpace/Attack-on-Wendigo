using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    public static float velocityMagnitude;
    public static float speedMagnitude;

    [SerializeField] private WeaponOscillator weaponSin;

    private PlayerCharacter player;
    private CharacterController controller;

    private Vector3 velocity;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
        controller = GetComponent<CharacterController>();
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
        Vector3 direction = GetDirection();
        player.Speed = PlayerIsMoving(direction) ? AccelerateSpeed() : player.MinSpeed;

        velocity = direction * player.Speed;
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
        controller.Move(velocity * Time.deltaTime);

        velocityMagnitude = velocity.magnitude / player.MaxSpeed;
        speedMagnitude = Mathf.Max((velocity.magnitude - player.MinSpeed), 0f) / (player.MaxSpeed - player.MinSpeed);
    }
}
