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
    public Transform gunTip;
    public LayerMask grappleable;
    public LineRenderer lineRenderer;

    public float maxDistance;
    public float delayTime;

    public Transform target;

    public float coolDown;
    private float coolDownTimer;

    private bool grappling;

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
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lineRenderer.SetPosition(0, gunTip.position);
            lineRenderer.SetPosition(1, target.position);
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

        lineRenderer.enabled = true;
    }

    public void ExecuteGrapple()
    {

    }

    public void StopGrapple()
    {
        grappling = false;
        coolDownTimer = coolDown;
        lineRenderer.enabled = false;
    }
}
