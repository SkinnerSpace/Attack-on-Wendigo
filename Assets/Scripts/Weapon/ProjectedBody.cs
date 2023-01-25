using UnityEngine;

public class ProjectedBody
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public void Set(Vector3 position, Vector3 velocity, Vector3 acceleration)
    {
        this.position = position;
        this.velocity = velocity;
        this.acceleration = acceleration;
    }
}
