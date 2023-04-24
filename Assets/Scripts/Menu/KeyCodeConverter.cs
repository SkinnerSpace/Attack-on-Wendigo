using System.Collections.Generic;
using UnityEngine;

public static class KeyCodeConverter
{
    private static Dictionary<KeyCode, string> substitutes = new Dictionary<KeyCode, string>() 
    {
        { KeyCode.Mouse0, "Left mouse button" },
        { KeyCode.Mouse1, "Right mouse button" },
        { KeyCode.Mouse2, "Mouse wheel" }
    };

    public static string Convert(KeyCode keyCode)
    {
        string substitute = substitutes.ContainsKey(keyCode) ? substitutes[keyCode] : keyCode.ToString();
        return substitute;
    }
}