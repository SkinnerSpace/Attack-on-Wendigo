using UnityEngine;
using System;
using TMPro;

public abstract class MenuButton : MenuElement
{
    protected TextMeshProUGUI label;

    public event Action onClick;
    public event Action onSelect;
    protected Action onCommand;

    protected bool isSelected;

    protected virtual void Awake(){
        label = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void SwitchOff()
    {
        Deselect();
        gameObject.SetActive(false);
    }

    public virtual void Click()
    {
        InvokeOnClick();
        onCommand();
    }

    protected void InvokeOnClick() => onClick?.Invoke();

    public virtual void Select()
    {
        isSelected = true;

        label.color = Color.green;
        label.rectTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
        onSelect?.Invoke();
    }

    public virtual void Deselect()
    {
        isSelected = false;

        label.color = Color.white;
        label.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
}
