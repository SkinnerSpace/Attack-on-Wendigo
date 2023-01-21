using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestroyer : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private void OnTriggerEnter(Collider other)
    {
        CollapseController collapsible = other.GetComponent<CollapseController>();
        collapsible.PullDown(pivot.forward);
    }
}
