using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenu
{
    [SerializeField] private MenuSFXPlayer sFXPlayer;
    [SerializeField] private MenuTitle title;
    [SerializeField] private List<Menu> menus;

    private Dictionary<string, Menu> Menus = new Dictionary<string, Menu>();

    private MenuButtonInteraction[] buttons;
    private List<MenuButtonInteraction> requiredButtons = new List<MenuButtonInteraction>();
    private List<MenuButtonInteraction> redundantButtons = new List<MenuButtonInteraction>();

    private Menu current;

    private void Awake()
    {
        buttons = GetComponentsInChildren<MenuButtonInteraction>();
        InitializeMenus();
        ConnectSFXPlayerToButtons();
    }

    private void Start(){
        MenuEvents.current.onBackToMenu += BackToMainMenu;
        MenuEvents.current.onSettings += () => Open("Settings");
    }

    private void InitializeMenus()
    {
        foreach (Menu menu in menus){
            Menus.Add(menu.name, menu);
        }
    }

    public void Open(string menuName)
    {
        DoOpen(menuName);
        sFXPlayer.PlayMenuOpen();
    }

    public void OpenSilently(string menuName) => DoOpen(menuName);

    private void DoOpen(string menuName)
    {
        CustomCursor.Instance.Unlock();
        gameObject.SetActive(true);

        CloseCurrentMenu();
        current = Menus[menuName];
        current.container.alpha = 1f;
        current.container.enabled = true;

        title.Set(Menus[menuName].title);
        SortButtons(Menus[menuName].buttons);
        EnableRequiredButtons();
        DisableRedundantButtons();
    }

    public void Close()
    {
        CustomCursor.Instance.Lock();

        CloseCurrentMenu();
        sFXPlayer.PlayMenuClose();
        gameObject.SetActive(false);
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
        foreach (MenuButtonInteraction button in requiredButtons){
            button.SwitchOn();
        }
    }

    private void DisableRedundantButtons()
    {
        foreach (MenuButtonInteraction button in redundantButtons){
            button.SwitchOff();
        }
    }


    private void ConnectSFXPlayerToButtons()
    {
        foreach (MenuButtonInteraction button in buttons){
            button.onClick += sFXPlayer.PlayButtonClick;
            button.onSelect += sFXPlayer.PlayButtonSelect;
        }
    }

    private void CloseCurrentMenu()
    {
        if (current != null){
            current.container.alpha = 0f;
            current.container.enabled = false;
        }
    }

    private void BackToMainMenu()
    {
        if (GameState.PauseMode == PauseMode.None){
            Open("Main");
        }
        else if (GameState.PauseMode == PauseMode.Paused){
            Open("Pause");
        }
    }
}