using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralizedObjects : MonoBehaviour
{
    [SerializeField] private List<Transform> objects = new List<Transform>();

    public void Centralize(Vector3 center)
    {
        foreach (Transform transform in objects)
            transform.position = center;
    }
}
