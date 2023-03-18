using System;
using UnityEngine;

public class HelicopterTimer : MonoBehaviour
{
    private float travelTime;
    private float currentTime;
    private float completion;

    private event Action<float> onTimeUpdate;

    public void Subscribe(IHelicopterTimeObserver observer)
    {
        onTimeUpdate += observer.UpdateCompletion;
    }

    public void Set(float distance, float speed)
    {
        currentTime = 0f;
        travelTime = distance / speed;
    }

    public void UpdateTime()
    {
        currentTime += OldChronos.DeltaTime;
        completion = (currentTime / travelTime).Clamp01();

        onTimeUpdate?.Invoke(completion);
    }
}


