using UnityEngine;

public class FlatMovement : MonoBehaviour
{
    private IController controller;

    private void Awake()
    {
        controller = GetComponent<IController>();
    }

    public Vector3 Calculate(float speed)
    {
        Vector3 rawDirection = controller.GetRawDirection();
        Vector3 velocity = (transform.right * rawDirection.x) + (transform.forward * rawDirection.z);
        velocity = velocity.normalized * speed;

        return velocity;
    }
}
