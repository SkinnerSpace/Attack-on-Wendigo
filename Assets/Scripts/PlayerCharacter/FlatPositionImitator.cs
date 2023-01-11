using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatPositionImitator : MonoBehaviour
{
    [SerializeField] private Transform body;

    private void Update()
    {
        transform.position = new Vector3(body.position.x, 0f, body.position.z);
        transform.eulerAngles = new Vector3(0f, body.eulerAngles.y, 0f);
    }
}
