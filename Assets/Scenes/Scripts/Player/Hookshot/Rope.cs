using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float length;

    public void Lengthen(float unit, float maxLength)
    {
        length += unit * Time.deltaTime;
        length = Mathf.Min(maxLength, length);
        SetScale();
    }

    public void Shorten(float unit, float minLength)
    {
        length -= unit * Time.deltaTime;
        length = Mathf.Max(length, minLength);
        SetScale();
    }

    private void SetScale()
    {
        transform.localScale = new Vector3(1f, 1f, length);
    }

    public void LookAt(Vector3 position)
    {
        transform.LookAt(position);
    }
}
