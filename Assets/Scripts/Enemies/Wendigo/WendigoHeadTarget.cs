using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WendigoHeadTarget : MonoBehaviour
{
    [SerializeField] private RigController rigController;

    private Transform target;
    private Vector3 velocity;

    private void Update()
    {
        if (target != null) 
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, 0.2f);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;

        if (target != null) rigController.SwitchOn();
        else rigController.SwitchOff();
    }
}
