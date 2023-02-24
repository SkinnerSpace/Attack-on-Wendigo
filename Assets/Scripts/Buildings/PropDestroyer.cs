using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestroyer : MonoBehaviour
{
    private Collider destroyerCollider;

    private void Awake()
    {
        destroyerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CollapseController collapsible = other.GetComponent<CollapseController>();

        Vector3 itsPosition = new Vector3(collapsible.Position.x, 0f, collapsible.Position.z);
        Vector3 ownPosition = new Vector3(transform.position.x, 0f, transform.position.z);

        Vector3 collapseDirection = (itsPosition - ownPosition).normalized;

        collapsible.PullDown(collapseDirection);
    }

    public void SwitchOff() => destroyerCollider.enabled = false;
}
