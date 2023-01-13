using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoAnimationController : MonoBehaviour
{
    private static int speedParam = Animator.StringToHash("Speed");

    [SerializeField] private Animator animator;

    public void UpdateSpeed(float wendigoSpeed)
    {
        animator.SetFloat(speedParam, wendigoSpeed);
    }
}
