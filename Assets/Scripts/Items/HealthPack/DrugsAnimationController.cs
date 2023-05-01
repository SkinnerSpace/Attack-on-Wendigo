using System.Collections.Generic;
using UnityEngine;

public class DrugsAnimationController : MonoBehaviour
{
    private static int injectTrigger = Animator.StringToHash("Inject");
    private static int isFreeBool = Animator.StringToHash("IsFree");

    [SerializeField] private Drugs drugs;
    [SerializeField] private DrugsPooledObject pooledObject;
    [SerializeField] private List<Animator> animators;

    private void Awake()
    {
        drugs.onInjection += PlayInject;
        pooledObject.onSpawn += ResetState;

        drugs.SubscribeOnReady(PlayIdle);
        drugs.SubscribeOnNotReady(ResetState);

        ResetState();
    }

    private void PlayIdle()
    {
        foreach (Animator animator in animators){
            animator.SetBool(isFreeBool, false);
        }
    }

    private void PlayInject()
    {
        foreach (Animator animator in animators){
            animator.SetTrigger(injectTrigger);
        }
    }

    private void ResetState()
    {
        foreach (Animator animator in animators){
            animator.ResetTrigger(injectTrigger);
            animator.SetBool(isFreeBool, true);
        }
    }
}
