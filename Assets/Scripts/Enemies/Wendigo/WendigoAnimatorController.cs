using UnityEngine;

public class WendigoAnimatorController :  IWendigoMovementObserver
{
    private static int velocity = Animator.StringToHash("Speed");
    private Animator animator;

    public WendigoAnimatorController(Wendigo wendigo)
    {
        animator = wendigo.Animator;
    }

    public void OnVelocityUpdate(float velocityMagnitude)
    {
        animator.SetFloat(velocity, velocityMagnitude);
    }
}
