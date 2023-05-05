﻿using System;
using UnityEngine;

public class HelicopterEvents : MonoBehaviour
{
    public static HelicopterEvents current;

    public event Action onMovedForTheFirstTime;
    public event Action onIsGoingToLand;
    public event Action onBoarded;
    public event Action onIsGoingToSetOff;
    public event Action onFlewAway;

    private void Awake()
    {
        current = this;
    }

    public void NotifyOnMovedForTheFirstTime() => onMovedForTheFirstTime?.Invoke();
    public void NotifyOnIsGoingToLand() => onIsGoingToLand?.Invoke();
    public void NotifyOnBoarded() => onBoarded?.Invoke();

    public void NotifyOnIsGoingToSetOff() => onIsGoingToSetOff?.Invoke();
    public void NotifyOnFlewAway() => onFlewAway?.Invoke();
}
