using UnityEngine;

public class CrateLandingController : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private LaserBeam laserBeam;
    [SerializeField] private Crate crate;
    [SerializeField] private CratePhysics physics;
    [SerializeField] private CrateSFXPlayer sFXPlayer;
    [SerializeField] private float buggyCollisionRadius = 1.5f;

    [Header("Settings")]
    [SerializeField] private float jumpForce = 7f;

    private bool isGrounded;
    private bool isDisabled;

    private void Update()
    {
        if (isGrounded){
            SwitchOnLaserWhenAtRest();
            JumpIfInsideACollider();
        }
    }

    private void SwitchOnLaserWhenAtRest()
    {
        if (!isDisabled && physics.IsAtRest()){
            isDisabled = true;
            laserBeam.SwitchOn();
        }
    }

    private void JumpIfInsideACollider(){
        if (Physics.CheckSphere(transform.position, buggyCollisionRadius, ComplexLayers.Ground)){
            Jump();
            sFXPlayer.PlayJump();
        }
    }

    public void ResetLanding()
    {
        isGrounded = false;
        isDisabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        JumpIfTheSurfaceIsInappropriate(collision);
        HitTheSurface(collision);
        sFXPlayer.PlayBump();
    }

    private void JumpIfTheSurfaceIsInappropriate(Collision collision)
    {
        if (SurfaceIsAppropriate(collision)){
            isGrounded = true;
        }
        else{
            Jump();
        }
    }

    private bool SurfaceIsAppropriate(Collision collision){
        return collision.gameObject.layer == (int)Layers.Landscape;
    }

    private void Jump()
    {
        isGrounded = false;

        physics.SetVelocity(
            new Vector3(x: Rand.Range(-0.5f, 0.5f),
                        y: Rand.Range(0.6f, 1f),
                        z: Rand.Range(-0.5f, 0.5f))
                           * jumpForce
                        );
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, buggyCollisionRadius);
    }
#endif
}
