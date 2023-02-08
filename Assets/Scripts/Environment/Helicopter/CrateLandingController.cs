using UnityEngine;

public class CrateLandingController : MonoBehaviour
{
    [SerializeField] private LaserBeam laserBeam;

    private Rigidbody physics;
    private bool isGrounded;
    private bool isDisabled;

    private void Awake() => physics = GetComponent<Rigidbody>();

    private void Update()
    {
        if (!isDisabled && isGrounded && physics.velocity.magnitude <= 0f)
        {
            isDisabled = true;
            physics.isKinematic = true;
            physics.useGravity = false;

            laserBeam.SwitchOn();
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision) => isGrounded = true;
}
