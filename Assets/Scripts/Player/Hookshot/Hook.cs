using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public enum State
    {
        Idle,
        Throwing,
        Grappled,
        Pulling
    }

    public LayerMask grappleableSurface;

    public float maxDistance;
    public float delayTime;

    public Transform target;

    public float throwTime;
    public float throwSpeed = 5f;
    public Vector3 position;

    public void StopGrapple()
    {
        /*
        state = State.Idle;
        coolDownTimer = coolDown;
        */
    }
}
