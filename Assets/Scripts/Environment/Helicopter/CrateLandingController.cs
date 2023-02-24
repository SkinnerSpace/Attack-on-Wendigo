using UnityEngine;

public class CrateLandingController : MonoBehaviour
{
    [SerializeField] private LaserBeam laserBeam;
    [SerializeField] private Crate crate;
    [SerializeField] private float jumpForce = 10f;

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

    Vector3 collisionPoint;

    private void OnCollisionEnter(Collision collision)
    {
        Surface surface = collision.transform.GetComponent<Surface>();

        collisionPoint = collision.contacts[0].point;

        foreach (ContactPoint contact in collision.contacts)
            collisionPoint = collisionPoint.Average(contact.point);
        
        if (collision.gameObject.layer == (int)Layers.Landscape)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            physics.velocity = new Vector3(0f, jumpForce, 0f);
        }
    }

    /*private void HitTheSurface()
    {
        Surface surface = probe.surface;
        Vector3 position = probe.position;
        Vector3 direction = data.FlatVelocity.normalized;

        surface.Hit().
                WithPosition(position).
                WithAngle(direction, Vector3.up).
                WithShape(1f, 70f).
                WithCount(5, 10).
                Launch();
    }*/
}
