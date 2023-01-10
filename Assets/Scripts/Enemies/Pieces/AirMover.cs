using UnityEngine;

public class AirMover : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float distance = 30f;

    [Header("Required components")]
    [SerializeField] private FlyingEnemy character;

    public Vector3 velocity { get; private set; }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 vecToTarget = character.Target.position - transform.position;
        Vector3 flatVecToTarget = new Vector3(vecToTarget.x, 0f, vecToTarget.z);

        float distanceToTarget = flatVecToTarget.magnitude;

        velocity = (distanceToTarget > distance) ? (transform.forward * speed) : Vector3.zero;
    }
}

