using UnityEngine;

public class CrateLandingController : MonoBehaviour
{
    [SerializeField] private LaserBeam laserBeam;
    [SerializeField] private Crate crate;

    private Rigidbody physics;
    private bool isGrounded;
    private bool isDisabled;

    private void Awake() => physics = GetComponent<Rigidbody>();

    private void Update()
    {
        if (!isDisabled && isGrounded && physics.velocity.magnitude <= 0f)
        {
            isDisabled = true;
            crate.ResetPhysics();
            laserBeam.SwitchOn();
        }
    }

    public void ResetLanding()
    {
        isGrounded = false;
        isDisabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        Surface surface = collision.transform.GetComponent<Surface>();
        if (surface != null)
        {
            Debug.Log(surface);
        }
    }
}
