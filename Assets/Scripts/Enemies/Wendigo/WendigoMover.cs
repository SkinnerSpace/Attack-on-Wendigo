using System;
using UnityEngine;

public class WendigoMover : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private WendigoAnimationController animationController;

    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private float deceleration = 0.5f;

    public float velocityMagn => velocity.magnitude;
    public Vector3 velocity { get; private set; }

    public Action<float> velocityUpdate;

    private void Awake()
    {
        animationController = GetComponent<WendigoAnimationController>();
        velocityUpdate += animationController.UpdateSpeed;
    }

    private void Update()
    {
        //transform.position += velocity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        Decelerate();

        velocityUpdate(velocity.magnitude);
    }

    private void Decelerate()
    {
        velocity = Vector3.Lerp(velocity, Vector3.zero, acceleration);
    }

    public void MoveForward()
    {
        Vector3 maxVelocity = transform.forward * speed;
        velocity = Vector3.Lerp(velocity, maxVelocity, acceleration);

        velocityUpdate(velocity.magnitude);
    }
}
