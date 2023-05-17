using UnityEngine;

public class TitleAnimator : MonoBehaviour
{
    private static int notPresentAnimation = Animator.StringToHash("NotPresent");
    private static int appearAnimation = Animator.StringToHash("Appear");

    [SerializeField] private Animator animator;

    public void Appear()
    {
        animator.Play(appearAnimation);
    }

    public void ResetState()
    {
        animator.Play(notPresentAnimation);
    }
}
