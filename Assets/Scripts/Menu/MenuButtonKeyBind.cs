using System;
using UnityEngine;

public class MenuButtonKeyBind : MenuButton
{
    [SerializeField] private KeyActions action;
    [SerializeField] private string actionName;

    private KeyBinds keys;

    public event Action<Action, KeyActions> onKeyHandled;

    private void Start(){
        onCommand = () => onKeyHandled?.Invoke(UpdateName, action);
    }

    public void UpdateName(KeyBinds keys)
    {
        this.keys = keys;
        UpdateName();
    }

    private void UpdateName(){
        string text = actionName + ": " + keys.GetBind(action);
        label.text = text;
    }

}