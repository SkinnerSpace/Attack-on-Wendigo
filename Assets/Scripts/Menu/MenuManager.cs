using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenu
{
    private MenuButtonInteraction[] buttons;

    [SerializeField] private MenuSFXPlayer sFXPlayer;

    [SerializeField] private List<Menu> menus;
    private Dictionary<string, List<MenuButtonInteraction>> Menus = new Dictionary<string, List<MenuButtonInteraction>>();

    private List<MenuButtonInteraction> requiredButtons = new List<MenuButtonInteraction>();
    private List<MenuButtonInteraction> redundantButtons = new List<MenuButtonInteraction>();

    private void Awake()
    {
        buttons = GetComponentsInChildren<MenuButtonInteraction>();
        
        foreach (Menu menu in menus)
            Menus.Add(menu.name, menu.buttons);

        PassSFXPlayerToTheButtons();
    }

    public void OpenWithSFX(string name){
        Open(name);
        sFXPlayer.PlayMenuOpen();
    }

    public void Open(string name)
    {
        CustomCursor.Instance.Unlock();
        gameObject.SetActive(true);

        SortButtons(Menus[name]);
        EnableRequiredButtons();
        DisableRedundantButtons();
    }

    private void SortButtons(List<MenuButtonInteraction> menu)
    {
        requiredButtons.Clear();
        redundantButtons.Clear();

        foreach (MenuButtonInteraction button in buttons)
        {
            if (menu.Contains(button))
                requiredButtons.Add(button);
            else
                redundantButtons.Add(button);
        }
    }

    private void EnableRequiredButtons()
    {
        foreach (MenuButtonInteraction button in requiredButtons)
            button.gameObject.SetActive(true);
    }

    private void DisableRedundantButtons()
    {
        foreach (MenuButtonInteraction button in redundantButtons)
            button.gameObject.SetActive(false);
    }

    public void Close()
    {
        CustomCursor.Instance.Lock();
        sFXPlayer.PlayMenuClose();
        gameObject.SetActive(false);
    }

    private void PassSFXPlayerToTheButtons()
    {
        foreach (MenuButtonInteraction button in buttons){
            button.SetSFXPlayer(sFXPlayer);
        }
    }
}
