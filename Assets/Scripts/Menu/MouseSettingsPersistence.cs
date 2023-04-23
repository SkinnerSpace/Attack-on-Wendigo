using UnityEngine;

public static class MouseSettingsPersistence
{
    private const string MOUSE_SENSITIVITY = "MouseSensitivity";
    private const string MOUSE_INVERSION = "MouseInversion";

    public static void Save(MouseSettings settings)
    {
        PlayerPrefs.SetFloat(MOUSE_SENSITIVITY, settings.mouseSensitivity);
        PlayerPrefs.SetInt(MOUSE_INVERSION, settings.mouseInversion ? 1 : 0);
    }

    public static void Load(MouseSettings settings)
    {
        float sensitivity = PlayerPrefs.HasKey(MOUSE_SENSITIVITY) ? PlayerPrefs.GetFloat(MOUSE_SENSITIVITY) : 100f;
        settings.LoadMouseSensitivity(sensitivity);

        int inversionInt = PlayerPrefs.HasKey(MOUSE_INVERSION) ? PlayerPrefs.GetInt(MOUSE_INVERSION) : 0;
        bool inversion = inversionInt == 1 ? true : false;
        settings.LoadMouseInversion(inversion);
    }
}