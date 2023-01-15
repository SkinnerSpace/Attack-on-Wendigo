using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Collapsible collapsible = other.GetComponent<Collapsible>();
        collapsible.Collapse();
    }
}
