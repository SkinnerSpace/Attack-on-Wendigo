using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    void Update()
    {
        transform.position = pivot.position;
    }
}
