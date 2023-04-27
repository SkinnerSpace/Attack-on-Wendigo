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
                string storedCode = PlayerPrefs.GetString(storedKey);
                KeyCode code = (KeyCode)Enum.Parse(typeof(KeyCode), storedCode, true); // ignore case
                binds.Bind(action, code);
            }
        }
    }
}