using UnityEngine;
using System;

[Serializable]
public class HelicopterDoorData
{
    [SerializeField] private float slideTime = 0.5f;
    public float SlideTime => slideTime;

    public float startTime { get; private set; }
    public float currentTime { get; set; }
    public float slidePhase { get; set; }

    public void SetStartTime(float startTime)
    {
        this.startTime = startTime;
        currentTime = 0f;
    }
}
