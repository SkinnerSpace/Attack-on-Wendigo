using UnityEngine;
using WendigoCharacter;

public class WendigoAnimationController : WendigoPlugableComponent
{
    private enum Animations
    {
        Idle,
        Walk
    }
    private Animations animation;

    // Booleans
    private static int isWalkingBool = Animator.StringToHash("IsWalking");

    public static int castTrigger = Animator.StringToHash("Cast");
    public static int firebreathTrigger = Animator.StringToHash("Firebreath");
    private Animator animator;

    public override void Initialize(Wendigo wendigo)
    {
        animator = wendigo.Animator;
    }

    public void OnVelocityUpdate(float velocityDelta)
    {
        if (animation == Animations.Walk){
            animator.speed = velocityDelta;
        }
    }

    public void PlayCastAnimation()
    {
        animator.speed = 1f;
        animator.SetTrigger(castTrigger);
    }

    public void PlayFirebreathAnimation()
    {
        animator.speed = 1f;
        animator.SetTrigger(firebreathTrigger);
    }

    public void SetIsWalking(bool isWalking)
    {
        animator.SetBool(isWalkingBool, isWalking);

        if (isWalking)
        {
            animation = Animations.Walk;
        }
        else
        {
            animation = Animations.Idle;
        }
    }

    public void ResetState()
    {
        animation = Animations.Idle;
        animator.speed = 1f;
    }
}
