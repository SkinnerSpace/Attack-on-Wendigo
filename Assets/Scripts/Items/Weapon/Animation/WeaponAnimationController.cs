using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    private static int shootAnim = Animator.StringToHash("Shoot");

    [SerializeField] private Weapon weapon;
    [SerializeField] private List<Animator> animators;

    private void Start()
    {
        weapon.SubscribeOnShot(PlayAnimation);
    }

    private void PlayAnimation()
    {
        foreach(Animator animator in animators){
            animator.Play(shootAnim, -1, 0f);
        }
    }
}
