using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Player character; 
    public Transform mainCamera;
    public LayerMask grappleable;

    public float maxDistance;
    public float delayTime;

    public Transform target;

    public float coolDown;
    private float coolDownTimer;

    private bool grappling;

    [SerializeField] private Rope rope;

    private IController controller;

    private void Awake()
    {
        controller = transform.parent.transform.parent.GetComponent<IController>();
    }

    private void Update()
    {
        if (controller.HookshotButtonIsPressed())
            StartGrapple();

        if (coolDownTimer > 0)
            coolDownTimer -= Time.deltaTime;

        if (grappling)
        {
            Lengthen();
            rope.LookAt(target.position);
        }
        else
        {
            Shorten();
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            
        }
    }

    public void StartGrapple()
    {
        if (coolDownTimer > 0f) return;

        grappling = true;

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, maxDistance, grappleable))
        {
            target.position = hit.point;
            target.SetParent(hit.transform);

            Invoke(nameof(ExecuteGrapple), delayTime);
        }
        else
        {
            target.position = mainCamera.position + (mainCamera.forward * maxDistance);
            Invoke(nameof(StopGrapple), delayTime);
        }
    }

    public void Lengthen()
    {
        float distanceToTarget = Vector3.Distance(rope.transform.position, target.position);
        rope.Lengthen(distanceToTarget, distanceToTarget);
        //Debug.Log(distanceToTarget);
    }

    public void Shorten()
    {
        float distanceToTarget = Vector3.Distance(rope.transform.position, target.position);
        rope.Shorten(distanceToTarget, 0.2f);
    }

    public void ExecuteGrapple()
    {
        
    }

    public void StopGrapple()
    {
        grappling = false;
        coolDownTimer = coolDown;
    }
}
