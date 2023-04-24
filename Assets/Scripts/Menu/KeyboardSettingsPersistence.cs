using System;
using UnityEngine;

public static class KeyboardSettingsPersistence
{
    public static void Save(KeyBinds binds)
    {
        foreach (KeyActions action in Enum.GetValues(typeof(KeyActions))){
            PlayerPrefs.SetString(action.ToString(), binds.keyActionPairs[action].ToString());
        }
    }

    public static void Load(KeyBinds binds)
    {
        foreach (KeyActions action in Enum.GetValues(typeof(KeyActions)))
        {
            string storedKey = action.ToString();

            if (PlayerPrefs.HasKey(storedKey)){
                KeyCode code = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(storedKey), true);
                binds.Bind(action, code);
            }
        }
    }
}