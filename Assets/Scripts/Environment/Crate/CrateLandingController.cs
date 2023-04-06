using UnityEngine;

public class CrateLandingController : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private LaserBeam laserBeam;
    [SerializeField] private Crate crate;
    [SerializeField] private CratePhysics physics;
    [SerializeField] private CrateSFXPlayer sFXPlayer;

    [Header("Settings")]
    [SerializeField] private float jumpForce = 7f;

    private bool isGrounded;
    private bool isDisabled;

    private void Update()
    {
        if (!isDisabled && isGrounded && physics.IsAtRest())
        {
            isDisabled = true;
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
        sFXPlayer.PlayBump();
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

            physics.SetVelocity(
                new Vector3(x: Rand.Range(-0.5f, 0.5f), 
                            y: Rand.Range(0.6f, 1f), 
                            z: Rand.Range(-0.5f, 0.5f)) 
                               * jumpForce
                );
        }
    }

    private void HitTheSurface(Collision collision)
    {
        ISurface surface = collision.transform.GetComponent<ISurface>();

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
