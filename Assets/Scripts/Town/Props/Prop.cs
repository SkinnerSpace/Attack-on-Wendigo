using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prop : MonoBehaviour
{
    [SerializeField] private float length; public float Length => length;
    [SerializeField] private float width; public float Width => width;
    [SerializeField] private float height; public float Height => height;

    [SerializeField] private List<float> angles = new List<float>();
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 1f;

    private void Awake()
    {
        SetRandomScale();
        SetRandomAngle();
    }

    private void SetRandomScale()
    {
        float scale = UnityEngine.Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void SetRandomAngle()
    {
        if (angles.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, angles.Count);
            transform.eulerAngles = new Vector3(0f, angles[index], 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f);
        }
    }
}
