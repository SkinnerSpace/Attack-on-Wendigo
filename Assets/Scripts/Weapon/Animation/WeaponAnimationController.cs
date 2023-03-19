using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    private static int shootAnim = Animator.StringToHash("Shoot");

    [SerializeField] private Animator animator;
    [SerializeField] private bool playAnimation;

    public void PlayShoot()
    {
        if (playAnimation){
            animator.Play(shootAnim, -1, 0f);
        }
    }
}
