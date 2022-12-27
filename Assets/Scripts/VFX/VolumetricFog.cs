using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricFog : MonoBehaviour
{
    [SerializeField] private Transform holder;

    void Update()
    {
        if (holder == null)
            return;

        Vector3 position = new Vector3(holder.position.x, 0f, holder.position.z);
        transform.position = position;
    }
}
