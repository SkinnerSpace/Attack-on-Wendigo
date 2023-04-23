using UnityEngine;
using System;
using TMPro;

public abstract class MenuButton : MenuElement
{
    protected TextMeshProUGUI label;

    public event Action onClick;
    public event Action onSelect;

    protected Action onCommand;

    private void Awake(){
        label = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void SwitchOff()
    {
        Deselect();
        gameObject.SetActive(false);
    }

    public void Click()
    {
        onClick?.Invoke();
        onCommand();
    }

    public void Select()
    {
        label.color = Color.green;
        label.rectTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
        onSelect?.Invoke();
    }

    public void Deselect()
    {
        label.color = Color.white;
        label.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
}
