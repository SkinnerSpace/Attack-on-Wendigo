using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviorController : MonoBehaviour
{
    [SerializeField] private Rigidbody physics;
    [SerializeField] private List<LayerChanger> layerChangers;

    public void GetReadyToHands()
    {
        SetPhysics(false);
        SwapTheLayers();
    }

    public void ThrowAway(Vector3 force)
    {
        SetPhysics(true);
        SwapTheLayers();
        physics.AddForce(force);
    }

    private void SetPhysics(bool active)
    {
        physics.isKinematic = !active;
        physics.useGravity = active;
    }

    private void SwapTheLayers()
    {
        foreach (LayerChanger changer in layerChangers)
            changer.SwapTheLayer();
    }
}
