using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WendigoHeadTarget : MonoBehaviour
{
    [SerializeField] private Wendigo wendigo;
    [SerializeField] private RigController rigController;
    [SerializeField] private InSightChecker inSightChecker;
    [SerializeField] private Transform defaultPoint;

    private Transform target;
    private Vector3 velocity;
    private Vector3 targetPosition;

    private bool isReady;



    private void Update()
    {
        /*if (wendigo.Data.Target != null)
        {
            targetPosition = TargetIsVisible() ? target.position : defaultPoint.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.5f);
        }*/
    }

    private bool TargetIsVisible() => (target != null) && inSightChecker.TargetIsVisibleFromPointOfView(target);

    public void SetTarget(Transform target)
    {
        this.target = target;

        if (target != null) rigController.SwitchOn();
        else rigController.SwitchOff();
    }
}
