using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    private static int shootAnim = Animator.StringToHash("Shoot");

    [Header("Required Components")]
    [SerializeField] private List<Animator> animators;
    [SerializeField] private FunctionTimer timer;

    [Header("Settings")]
    [SerializeField] private bool isAnimated;
    [SerializeField] private float delay;

    public void PlayAnimationIfPossible()
    {
        if (isAnimated){
            if (delay <= 0f)
            {
                PlayAnimation();
            }
            else
            {
                timer.Set("Play", delay, PlayAnimation);
            }
        }
    }

    private void PlayAnimation()
    {
        foreach(Animator animator in animators){
            animator.Play(shootAnim, -1, 0f);
        }
    }
}
