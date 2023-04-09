using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestroyer : MonoBehaviour, IAmputationObserver
{
    private Collider destroyerCollider;

    private void Awake()
    {
        destroyerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollapsible collapsible = other.GetComponent<ICollapsible>();

        Vector3 itsPosition = new Vector3(collapsible.Position.x, 0f, collapsible.Position.z);
        Vector3 ownPosition = new Vector3(transform.position.x, 0f, transform.position.z);

        Vector3 collapseDirection = (itsPosition - ownPosition).normalized;

        collapsible.PullDown(collapseDirection);
    }

    public void OnAmputation()
    {
        SwitchOff();
    }

    public void SwitchOff() => destroyerCollider.enabled = false;
    public void SwitchOn() => destroyerCollider.enabled = true;
}
