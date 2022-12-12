using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LegPoint : RigPoint
{
    public enum Side
    {
        Left = -1,
        Right = 1
    }
    [SerializeField] public Side side;

    private Vector3 oldPosition;
    private Vector3 currentPosition;
    private Vector3 newPosition;
    private float lerp = 0f;

    [SerializeField] protected BipedalController controller;

    private void Awake()
    {
        currentPosition = transform.position;
        oldPosition = currentPosition;
        newPosition = currentPosition;

        controller.AddRigPoint(this);
    }

    public void Walk()
    {
        transform.position = currentPosition;

        RaycastHit hitInfo = GetHitPoint();
        SetNewPositionIfDistanceTooBig(hitInfo.point);

        if (lerp < 1f)
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * controller.StepHeight;
            currentPosition = footPosition;
            lerp += Time.deltaTime * controller.Speed;
        }
        else
        {
            oldPosition = newPosition;
            controller.Notify(this);
        }
    }

    private RaycastHit GetHitPoint()
    {
        Ray ray = new Ray(controller.transform.position + (controller.transform.right * controller.FootSpacing * (float) side), Vector3.down);
        Physics.Raycast(ray, out RaycastHit info, Mathf.Infinity, controller.GroundLayer.value);
        return info;
    }

    private void SetNewPositionIfDistanceTooBig(Vector3 bodyPosition)
    {
        if (Vector3.Distance(newPosition, bodyPosition) > controller.StepDistance)
        {
            lerp = 0f;
            newPosition = bodyPosition;
        }
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 2f);
        */
    }
}