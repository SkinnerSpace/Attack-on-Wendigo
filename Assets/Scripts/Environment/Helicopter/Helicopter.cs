using System;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private BezierTrajectory trajectory;
    [SerializeField] private float speed;
    private float distancePassed;
    public bool isMoving;

    private Action onFinish;

    private void Awake()
    {
        onFinish += Arrived;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (isMoving)
        {
            distancePassed += speed * Chronos.DeltaTime;
            transform.position = trajectory.GetPosition(distancePassed, onFinish);
        }
    }

    public void Arrived()
    {
        isMoving = false;
        distancePassed = 0f;
    }
}


