using UnityEngine;
using WendigoCharacter;

public class WendigoAnimationController : WendigoPlugableComponent
{
    private static int velocity = Animator.StringToHash("Speed");
    public static int castTrigger = Animator.StringToHash("Cast");
    public static int firebreathTrigger = Animator.StringToHash("Firebreath");
    private Animator animator;

    public override void Initialize(Wendigo wendigo)
    {
        animator = wendigo.Animator;
    }

    public void OnVelocityUpdate(float velocityMagnitude) => animator.SetFloat(velocity, velocityMagnitude);

    public void PlayCastAnimation() => animator.SetTrigger(castTrigger);
    public void PlayFirebreathAnimation() => animator.SetTrigger(firebreathTrigger);
}
