using System;
using UnityEngine;

public static class StateLogger
{
    private static bool isActive = true;

    public static void Log(string message)
    {
        if (isActive) Debug.Log(message);
    }
}
