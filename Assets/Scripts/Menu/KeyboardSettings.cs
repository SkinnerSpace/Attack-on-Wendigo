using UnityEngine;
using System;

public class KeyboardSettings : MonoBehaviour
{
    private MenuButtonKeyBind[] keyBinds;

    private Action onKeyHandled;
    private KeyActions currentAction;

    private KeyBinds keys;

    private void Awake()
    {
        InitializeKeyBindsButtons();
    }

    private void Start()
    {
        keys = KeyBinds.Instance;
        KeyboardSettingsPersistence.Load(keys);
        UpdateKeyBindsButtons();
    }

    private void OnGUI()
    {
        if (onKeyHandled != null){
            Event e = Event.current;

            if (e.isKey){
                keys.Bind(currentAction, e.keyCode);
                onKeyHandled?.Invoke();
                onKeyHandled = null;
            }
        }
    }

    private void HandleKey(Action onKeyHandled, KeyActions currentAction)
    {
        this.onKeyHandled = onKeyHandled;
        this.currentAction = currentAction;
    }

    private void InitializeKeyBindsButtons()
    {
        keyBinds = GetComponentsInChildren<MenuButtonKeyBind>();

        foreach (MenuButtonKeyBind keyBind in keyBinds){
            keyBind.onKeyHandled += HandleKey;
        }
    }

    private void UpdateKeyBindsButtons()
    {
        foreach (MenuButtonKeyBind keyBind in keyBinds){
            keyBind.UpdateName(keys);
        }
    }
}
