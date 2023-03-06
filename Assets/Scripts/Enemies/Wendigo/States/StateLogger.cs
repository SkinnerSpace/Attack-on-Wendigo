using System;
using UnityEngine;

public static class StateLogger
{
    private static bool isActive = false;

    public static void Log(string message)
    {
        if (isActive) Debug.Log(message);
    }
}
