using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        if (holder == null)
            return;

        Vector3 position = new Vector3(holder.position.x, 0f, holder.position.z);
        transform.position = position + offset;
    }
}
