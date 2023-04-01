using System.Collections.Generic;
using UnityEngine;

public class LimbAmputationNotifier : MonoBehaviour
{
    [SerializeField] private List<Limb> limbs;
    private IAmputationObserver observer;

    private int health;

    private void Awake()
    {
        observer = GetComponent<IAmputationObserver>();
        SubscribeOnLimbs();
    }

    private void SubscribeOnLimbs()
    {
        if (LimbsExist())
        {
            foreach (Limb limb in limbs)
            {
                limb.SubscribeOnAmputation(ReceiveDamage);
            }

            health = limbs.Count;
        }
    }

    private bool LimbsExist() => limbs != null && limbs.Count > 0;

    private void ReceiveDamage()
    {
        health -= 1;

        if (health <= 0){
            observer.OnAmputation();
        }
    }
}
