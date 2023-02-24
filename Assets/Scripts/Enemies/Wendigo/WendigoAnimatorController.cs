using UnityEngine;

public class WendigoAnimatorController : WendigoBaseController, IWendigoMovementObserver
{
    private static int velocity = Animator.StringToHash("Speed");
    private Animator animator;

    public override void Initialize(IWendigo wendigo)
    {
        animator = wendigo.Animator;
    }

    public void OnVelocityUpdate(float velocityMagnitude) => animator.SetFloat(velocity, velocityMagnitude);
}
