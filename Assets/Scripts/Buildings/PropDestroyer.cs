using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CollapseController collapsible = other.GetComponent<CollapseController>();

        Vector3 pushDir = transform.forward;
        collapsible.Push(pushDir);
    }
}
