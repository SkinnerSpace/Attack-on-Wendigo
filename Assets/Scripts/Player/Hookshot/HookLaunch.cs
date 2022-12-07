using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HookLaunch : Command
{
    private Hook hook;
    private Transform mainCamera;

    private void Awake()
    {
        hook = GetComponentInChildren<Hook>();
        mainCamera = GetComponentInChildren<Camera>().transform;
    }

    public override void Execute()
    {
        hook.position = transform.position;

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, hook.maxDistance, hook.grappleableSurface))
        {
            hook.target.position = hit.point;
            hook.target.SetParent(hit.transform);
        }
        else
        {
            hook.target.position = mainCamera.position + (mainCamera.forward * hook.maxDistance);
        }
    }
}
