using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DebugSphere : MonoBehaviour
{
    private Vector3 position;
    private Color color;
    private bool active;

    public void Draw(Vector3 position, Color color)
    {
        Draw(position);
        this.color = color;
    }

    public void Draw(Vector3 position)
    {
        this.position = position;
        active = true;
    }

    private void OnDrawGizmos()
    {
        if (active)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(position, 0.5f);
            active = false;
        }
    }
}
