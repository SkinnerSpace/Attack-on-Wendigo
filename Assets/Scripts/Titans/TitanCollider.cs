using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanCollider : MonoBehaviour
{
    private Transform body;

    private void Awake()
    {
        body = transform.root;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDestructible destructible = other.transform.GetComponent<IDestructible>();
        Vector3 direction = body.forward;

        if (destructible != null)
            destructible.Collapse(direction);
    }
}
