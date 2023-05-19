using UnityEngine;
using UnityEngine.UI;

public class MouseSettings : MonoBehaviour
{
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private Toggle mouseInversionToggle;

    public float mouseSensitivity { get; private set; }
    public bool mouseInversion { get; private set; }

    private KeyBinds controls;

    private void Start()
    {
        controls = KeyBinds.Instance;

        MouseSettingsPersistence.Load(this);
        MenuEvents.current.onSubMenuEnter += SaveSettingsOnExit;
        MenuEvents.current.onMenuClosed += SaveSettings;

        mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        mouseInversionToggle.onValueChanged.AddListener(SetMouseInversion);
    }

    public void LoadMouseSensitivity(float value){
        mouseSensitivitySlider.SetValueWithoutNotify(value);
        SetMouseSensitivity(value);
    }

    private void SetMouseSensitivity(float value)
    {
        mouseSensitivity = value;
        controls.mouseSensitivity = mouseSensitivity;
    }

    public void LoadMouseInversion(bool value){
        mouseInversionToggle.SetIsOnWithoutNotify(value);
        SetMouseInversion(value);
    }

    private void SetMouseInversion(bool value)
    {
        mouseInversion = value;
        controls.mouseInversion = mouseInversion;
    }

    private void SaveSettingsOnExit(string subMenuName){
        if (subMenuName == "controls"){
            SaveSettings();
        }
    }

    private void SaveSettings() => MouseSettingsPersistence.Save(this);
}
