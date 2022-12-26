using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IDestructible destructible = other.transform.GetComponent<IDestructible>();
        Vector3 direction = (other.transform.position - transform.position).normalized;

        if (destructible != null)
            destructible.Collapse(direction);
    }
}
