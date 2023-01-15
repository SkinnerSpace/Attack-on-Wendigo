using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collapsible : MonoBehaviour
{
    [SerializeField] private Transform root;

    public void Collapse()
    {
        Destroy(root.gameObject);
    }
}
