using UnityEngine;
using System;
using TMPro;

public class MenuButtonInteraction : MonoBehaviour
{
    [SerializeField] private MenuCommands command;
    [SerializeField] private TextMeshProUGUI label;

    private MenuSFXPlayer sFXPlayer;

    private Action onCommand;

    private void Start()
    {
        onCommand = MenuCommandsFactory.Create(command);
    }

    public void OnClick(){
        onCommand();
        sFXPlayer.PlayButtonClick();
    }

    public void Entered()
    {
        label.color = Color.green;
        label.rectTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
        sFXPlayer.PlayButtonSelect();
    }

    public void Exited()
    {
        label.color = Color.white;
        label.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void SetSFXPlayer(MenuSFXPlayer sFXPlayer) => this.sFXPlayer = sFXPlayer;
}
