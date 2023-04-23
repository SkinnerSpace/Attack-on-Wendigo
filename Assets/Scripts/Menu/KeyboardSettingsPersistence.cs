using UnityEngine;

public static class KeyboardSettingsPersistence
{
    public static void Save(KeyBinds keys)
    {
        foreach (KeyActions keyAction in keys.Keys.Keys){
            PlayerPrefs.SetString(keyAction.ToString(), keys.Keys[keyAction].ToString());
        }
    }

    public static void Load(KeyBinds keys)
    {
        foreach (KeyActions keyAction in keys.Keys.Keys)
        {
            string savedKey = keyAction.ToString();

            if (PlayerPrefs.HasKey(savedKey)){
                KeyCode code = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(savedKey), true);
                keys.Bind(keyAction, code);
                Debug.Log("Exist " + code);
            }

            PlayerPrefs.SetString(keyAction.ToString(), keys.Keys[keyAction].ToString());
        }
    }
}