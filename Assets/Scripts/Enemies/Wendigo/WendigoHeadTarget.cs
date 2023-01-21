using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoHeadTarget : MonoBehaviour
{
    private Transform target;
    private Vector3 velocity;

    private void Update()
    {
        if (target != null) 
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, 0.2f);
    }

    public void SetTarget(Transform target) => this.target = target;
}
