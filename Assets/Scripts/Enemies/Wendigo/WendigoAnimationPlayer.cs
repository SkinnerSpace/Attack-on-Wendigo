using UnityEngine;

public class WendigoAnimationPlayer : WendigoBaseController, IWendigoMovementObserver
{
    private static int velocity = Animator.StringToHash("Speed");
    public static int castTrigger = Animator.StringToHash("Cast");
    private Animator animator;

    public override void Initialize(IWendigo wendigo)
    {
        animator = wendigo.Animator;
    }

    public void OnVelocityUpdate(float velocityMagnitude) => animator.SetFloat(velocity, velocityMagnitude);

    public void PlayCastAnimation() => animator.SetTrigger(castTrigger);
}
