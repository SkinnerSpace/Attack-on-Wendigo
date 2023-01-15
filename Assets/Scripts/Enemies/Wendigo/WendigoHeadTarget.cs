using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoHeadTarget : MonoBehaviour
{
    private Transform target;

    private void Update()
    {
        if (target != null) transform.position = target.position;
    }

    public void SetTarget(Transform target) => this.target = target;
}
