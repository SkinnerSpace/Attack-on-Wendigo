using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float length { get; private set; }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void LookAt(Vector3 position)
    {
        transform.LookAt(position);
    }

    public void Lengthen(float units)
    {
        length += units * Time.deltaTime;
        transform.localScale = new Vector3(1, 1, length);
    }

    public void Shorten(float units)
    {
        length -= units * Time.deltaTime;
        transform.localScale = new Vector3(1, 1, length);
    }

    public void SetLength(float length)
    {
        this.length = length;
    }
}
