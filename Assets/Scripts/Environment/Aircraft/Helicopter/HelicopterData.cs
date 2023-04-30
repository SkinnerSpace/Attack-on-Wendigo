using System.Collections.Generic;
using UnityEngine;

public class HelicopterData : MonoBehaviour
{
    [Header("Fly")]
    public float speed = 40f;
    public float rotationSpeed = 5f;

    [Header("Idle Time")]
    public float emptyStorageIdleTime = 2f;
    public float landIdleTime = 1f;
    public float descendIdleTime = 0f;

    [Header("Descend")]
    public float descendTime;
    public float descendHeight;

    [Header("Ascend")]
    public float ascendTime;

    public bool isMoving { get; set; }
    public bool skipFrame { get; set; }

    public float currentDescendTime { get; set; }
    public float currentAscendTime { get; set; }

    public Quaternion rotation
    {
        get { 
            return transform.rotation; 
        }
        set { 
            transform.rotation = value; 
        }
    }

    public Quaternion targetRotation { get; set; }

    public Vector3 position
    {
        get{
            return transform.position;
        }
        set{
            transform.position = value;
        }
    }

    public Vector3 previousPosition { get; set; }
    public Vector3 positionBeforeDescend { get; set; }
    public Vector3 descendPosition { get; set; }

    public HelicopterStates state { get; set; } = HelicopterStates.Follow;
}

