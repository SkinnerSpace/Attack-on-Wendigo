using UnityEngine;

public class UpscaleAnimator : MonoBehaviour
{
    private static int defaultAnimation = Animator.StringToHash("Default");
    private static int increaseAnimation = Animator.StringToHash("Increase");

    [SerializeField] private Animator animator;

    public void Increase()
    {
        animator.Play(increaseAnimation);
    }

    public void ResetState()
    {
        animator.Play(defaultAnimation);
    }
}
