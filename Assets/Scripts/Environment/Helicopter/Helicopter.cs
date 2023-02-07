using System;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private BezierTrajectory trajectory;
    [SerializeField] private float speed;

    public float DistancePassed => distancePassed;
    public float RouteCompletion => (distancePassed / trajectory.Length);
    public float timeToComplete => (trajectory.Length - distancePassed) / speed;

    private float totalTime;
    private float distancePassed;
    private bool isMoving;

    private Action onFinish;

    private void Awake()
    {
        onFinish += Arrived;
    }

    private void Update()
    {
        Move();
    }

    public void Launch()
    {
        isMoving = true;
        totalTime = trajectory.Length / speed;
        Debug.Log(totalTime);
    }

    public void Stop() => isMoving = false;

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


