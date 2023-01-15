using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private PropShakeManager propShake;

    private bool collapsed;

    public void Collapse()
    {
        if (!collapsed && propShake != null)
        {
            collapsed = true;
            propShake.Launch();
        }
    }
}
