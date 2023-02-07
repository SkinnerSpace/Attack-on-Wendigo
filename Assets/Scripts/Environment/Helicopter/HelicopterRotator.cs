using UnityEngine;

public class HelicopterRotator : MonoBehaviour
{
    public Quaternion Rotate(Vector3 prevPos)
    {
        Vector3 direction = (transform.position - prevPos).normalized;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        //Debug.Log($"Dir {direction} Angle {angle}");
        return rotation;
    }
}
