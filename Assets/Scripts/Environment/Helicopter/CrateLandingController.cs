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
            crate.PrepareToBeUnpacked();
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
        JumpOffTheWrongSurface(collision);
        Vector3 hitPoint = GetHitPoint(collision);
        HitTheSurface(collision, hitPoint);
    }

    private Vector3 GetHitPoint(Collision collision)
    {
        Vector3 hitPoint = collision.contacts[0].point;

        foreach (ContactPoint contact in collision.contacts)
            hitPoint = hitPoint.Average(contact.point);

        return hitPoint;
    }

    private void JumpOffTheWrongSurface(Collision collision)
    {
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

    private void HitTheSurface(Collision collision, Vector3 hitPoint)
    {
        Surface surface = collision.transform.GetComponent<Surface>();

        if (surface != null)
        {
            surface.Hit().
                    WithPosition(hitPoint).
                    WithAngle(Vector3.down, Vector3.up).
                    WithShape(1f, 70f).
                    WithCount(5, 10).
                    Launch();
        }
    }
}
