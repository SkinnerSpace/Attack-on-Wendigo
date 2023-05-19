using UnityEngine;
using System;

public class KeyboardSettings : MonoBehaviour
{
    private MenuButtonKeyBind[] keyBinds;

    private Action onKeyHandled;
    private KeyActions currentAction;

    private KeyBinds keys;

    private Event myEvent = new Event();

    private KeyCode[] mouseKeys;

    private void Awake()
    {
        mouseKeys = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2 };
    }

    private void Start()
    {
        keys = KeyBinds.Instance;

        KeyboardSettingsPersistence.Load(keys);
        MenuEvents.current.onSubMenuEnter += SaveSettingsOnExit;
        MenuEvents.current.onMenuClosed += SaveSettings;

        InitializeButtons();
    }

    private void Update()
    {
        if (onKeyHandled != null){
            HandleInputEvent();
        }
    }

    private void HandleInputEvent()
    {
        while (Event.PopEvent(myEvent))
        {
            if (IsMouseButton(myEvent)){
                HandleMouseButton();
            }
            else if (IsKeyboardButton(myEvent)){
                BindKey(myEvent.keyCode);
            }
        }
    }

    private bool IsMouseButton(Event inputEvent){
        return myEvent.isMouse &&
               myEvent.type == EventType.MouseDown;
    }

    private void HandleMouseButton()
    {
        for (int i = 0; i < mouseKeys.Length; i++)
        {
            KeyCode currentKeyCode = mouseKeys[i];

            if (Input.GetKeyDown(currentKeyCode)){
                BindKey(currentKeyCode);
            }
        }
    }

    private bool IsKeyboardButton(Event inputEvent){
        return myEvent.isKey &&
               myEvent.type == EventType.KeyDown &&
               myEvent.keyCode != KeyCode.None;
    }

    private void BindKey(KeyCode keyCode){
        keys.Bind(currentAction, keyCode);
        onKeyHandled?.Invoke();
        onKeyHandled = null;
    }

    private void GetReadyToHandleInput(Action onKeyHandled, KeyActions currentAction)
    {
        this.onKeyHandled = onKeyHandled;
        this.currentAction = currentAction;
    }

    private void InitializeButtons()
    {
        keyBinds = GetComponentsInChildren<MenuButtonKeyBind>(true);

        foreach (MenuButtonKeyBind keyBind in keyBinds)
        {
            keyBind.onActionSelected += GetReadyToHandleInput;
            keyBind.InitializeButton(keys);
        }
    }

    private void SaveSettingsOnExit(string subMenuName)
    {
        if (subMenuName == "controls"){
            SaveSettings();
        }
    }

    private void SaveSettings() => KeyboardSettingsPersistence.Save(keys);
}
