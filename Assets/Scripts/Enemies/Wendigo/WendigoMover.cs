using UnityEngine;

public class WendigoMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    public void MoveForward()
    {
        Vector3 velocity = transform.forward * speed;
        transform.position += velocity * Time.deltaTime;
    }
}
