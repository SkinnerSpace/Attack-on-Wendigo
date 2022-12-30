using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HookRenderer : MonoBehaviour
{
    private Hook hook;
    private LineRenderer rope;

    private void Awake()
    {
        rope = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        rope.SetPosition(0, transform.position);
        rope.SetPosition(1, hook.position);
    }
}
