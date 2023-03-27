using UnityEngine;

public class Shoulder : MonoBehaviour
{
    [SerializeField] private Limb chest;
    [SerializeField] private Limb upperArm;

    private void Awake()
    {
        chest.SubscribeOnMutilation(ExposeGoreIfPossible);
    }

    public void ExposeGoreIfPossible()
    {
        if (!upperArm.IsDestroyed()){
            upperArm.ExposeGoreButKeepTheFleshUntouched();
        }
    }
}

