using UnityEngine;

public class SnowSurface : MonoBehaviour, ISurface
{
    public void Hit(Vector3 velocity, float radius)
    {
        Debug.Log(GetType());
        Debug.Log($"Velocity {velocity} Radius {radius}");
    }
}