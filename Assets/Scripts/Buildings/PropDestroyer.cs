using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destructible collapsible = other.GetComponent<Destructible>();
        collapsible.Collapse();
    }
}
