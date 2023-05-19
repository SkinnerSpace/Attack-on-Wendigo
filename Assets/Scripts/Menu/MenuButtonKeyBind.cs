using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonKeyBind : MenuButton
{
    [SerializeField] private KeyActions action;

    private Button button;
    private KeyBinds keys;

    public event Action<Action, KeyActions> onActionSelected;
    private string actionName;
    private bool isUsed;
    private bool isLocked;

    protected override void Awake(){
        base.Awake();
        button = GetComponentInChildren<Button>();
        actionName = label.text;
    }

    private void Start(){
        onCommand = () => onActionSelected?.Invoke(OnKeyBindChanged, action);
    }

    private void Update()
    {
        if (isLocked)
        {
            if (LeftMouseButtonIsNotPressed()){
                isLocked = false;
                button.enabled = true;
            }
        }
    }

    private bool LeftMouseButtonIsNotPressed()
    {
        return !Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0);
    }

    public void InitializeButton(KeyBinds keys)
    {
        this.keys = keys;
        UpdateName();
    }

    private void OnKeyBindChanged()
    {
        UpdateName();
        InvokeOnClick();

        isUsed = false;
        
        CancelSelectionIfNotUsedAndSelected();

        isLocked = true;
    }

    private void UpdateName(){
        KeyCode keyCode = keys.GetBind(action);

        string keyName = KeyCodeConverter.Convert(keyCode);
        string text = actionName + ": " + keyName;

        label.text = text;
    }

    public override void Click()
    {
        if (!isUsed && !isLocked){
            Use();
            base.Click();
        }
    }

    private void Use()
    {
        isUsed = true;
        button.enabled = false;

        string text = actionName + ": _";
        label.text = text;
    }

    private void Unlock() => isLocked = false;

    public override void Deselect()
    {
        isSelected = false;
        CancelSelectionIfNotUsedAndSelected();
    }

    private void CancelSelectionIfNotUsedAndSelected()
    {
        if (!isUsed && !isSelected){
            label.color = Color.white;
            label.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
