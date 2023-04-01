﻿using UnityEngine;

public abstract class LoggableState
{
    public static bool isActive = false;

    public virtual void LogEnter()
    {
        if (isActive)
            Debug.Log("Entered " + GetType().ToString());
    }

    public void LogExit()
    {
        if (isActive)
            Debug.Log("Exited " + GetType().ToString());
    }
}