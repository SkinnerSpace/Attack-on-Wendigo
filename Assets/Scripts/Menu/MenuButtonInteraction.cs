using UnityEngine;
using System;
using TMPro;

public class MenuButtonInteraction : MenuElement
{
    [SerializeField] private string command;
    private TextMeshProUGUI label;

    private Action onCommand;
    public Action onClick;
    public Action onSelect;

    private void Awake(){
        label = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start(){
        onCommand = MenuCommandsFactory.Create(command);
    }

    public override void SwitchOff(){
        Deselect();
        gameObject.SetActive(false);
    }

    public void Click(){
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
