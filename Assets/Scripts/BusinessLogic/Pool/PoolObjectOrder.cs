using UnityEngine;

public struct PoolObjectOrder
{
    public string tag;
    public Vector3 position;
    public Quaternion rotation;

    public PoolObjectOrder(string tag, Vector3 position, Quaternion rotation)
    {
        this.tag = tag;
        this.position = position;
        this.rotation = rotation;
    }
}



