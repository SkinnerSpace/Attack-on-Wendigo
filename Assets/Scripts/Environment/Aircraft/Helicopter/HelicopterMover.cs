using System;
using UnityEngine;

public class HelicopterMover : MonoBehaviour, IHelicopterTimeObserver
{
    private float completion;
    [SerializeField] private Helicopter helicopter;

    public void UpdateCompletion(float completion) => this.completion = Easing.QuadEaseInOut(completion);

    public Vector3 Move(BezierTrajectory trajectory, Action onFinish)
    {
        float distancePassed = Mathf.Lerp(0f, trajectory.Length, completion);

        Vector3 position = trajectory.GetPosition(distancePassed, onFinish);
        return position;
    }
}


