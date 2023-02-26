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
        HitTheSurface(collision);
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
            physics.velocity = new Vector3(Rand.Range(-0.5f, 0.5f), Rand.Range(0.6f, 1f), Rand.Range(-0.5f, 0.5f)) * jumpForce;
        }
    }

    private void HitTheSurface(Collision collision)
    {
        Surface surface = collision.transform.GetComponent<Surface>();

        if (surface != null)
        {
            surface.Hit().
                    WithPosition(collision.GetHitPoint()).
                    WithAngle(Vector3.down, Vector3.up).
                    WithShape(1f, 70f).
                    WithCount(5, 10).
                    Launch();
        }
    }
}
