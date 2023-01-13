using System;
using UnityEngine;

public class WendigoMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private WendigoAnimationController animationController;

    private Vector3 velocity;

    public Action<float> velocityUpdate;

    private void Awake()
    {
        animationController = GetComponent<WendigoAnimationController>();
        velocityUpdate += animationController.UpdateSpeed;
    }

    public void MoveForward()
    {
        velocity = transform.forward * speed;
        transform.position += velocity * Time.deltaTime;

        velocityUpdate(velocity.magnitude);
    }
}
