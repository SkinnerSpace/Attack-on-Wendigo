using UnityEngine;

public class WendigoAnimatorController : WendigoBaseController, IWendigoMovementObserver
{
    private static int velocity = Animator.StringToHash("Speed");
    public static int castBool = Animator.StringToHash("Cast");
    private Animator animator;

    public override void Initialize(IWendigo wendigo)
    {
        animator = wendigo.Animator;
    }

    public void OnVelocityUpdate(float velocityMagnitude) => animator.SetFloat(velocity, velocityMagnitude);

    public void PlayCastAnimation() => animator.SetBool(castBool, true);
    public void StopCastAnimation() => animator.SetBool(castBool, false);
}
