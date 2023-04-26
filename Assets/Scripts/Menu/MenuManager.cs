﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenu
{
    [SerializeField] private MenuSFXPlayer sFXPlayer;
    [SerializeField] private MenuTitle title;
    [SerializeField] private List<Menu> menus;

    private Dictionary<string, Menu> Menus = new Dictionary<string, Menu>();

    private MenuElement[] elements;
    private MenuButton[] buttons;
    private MenuToggleButton[] toggles;

    private List<MenuElement> requiredElements = new List<MenuElement>();
    private List<MenuElement> redundantElements = new List<MenuElement>();

    private Menu current;

    private void Awake()
    {
        elements = GetComponentsInChildren<MenuElement>();
        buttons = GetComponentsInChildren<MenuButton>();
        toggles = GetComponentsInChildren<MenuToggleButton>();

        InitializeMenus();
        ConnectSFXPlayerToButtons();
        ConnectSFXPlayerToToggles();
    }

    private void Start(){
        MenuEvents.current.onSubMenuEnter += Open;
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
        SortElements(Menus[menuName].buttons);
        EnableRequiredElements();
        DisableRedundantElements();
    }

    public void Close()
    {
        CustomCursor.Instance.Lock();

        CloseCurrentMenu();
        DisableAllTheElements();

        sFXPlayer.PlayMenuClose();
        gameObject.SetActive(false);
    }

    private void SortElements(List<MenuElement> menu)
    {
        requiredElements.Clear();
        redundantElements.Clear();

        foreach (MenuElement element in elements)
        {
            if (menu.Contains(element))
                requiredElements.Add(element);
            else
                redundantElements.Add(element);
        }
    }

    private void EnableRequiredElements()
    {
        foreach (MenuElement element in requiredElements){
            element.SwitchOn();
        }
    }

    private void DisableRedundantElements()
    {
        foreach (MenuElement element in redundantElements){
            element.SwitchOff();
        }
    }

    private void DisableAllTheElements()
    {
        foreach (MenuElement element in elements){
            element.SwitchOff();
        }
    }

    private void ConnectSFXPlayerToButtons()
    {
        foreach (MenuButton button in buttons){
            button.onClick += sFXPlayer.PlayButtonClick;
            button.onSelect += sFXPlayer.PlayButtonSelect;
        }
    }

    private void ConnectSFXPlayerToToggles()
    {
        foreach (MenuToggleButton toggle in toggles){
            toggle.onToggle += sFXPlayer.PlayButtonClick;
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
            Open("main");
        }
        else if (GameState.PauseMode == PauseMode.Paused){
            Open("pause");
        }
    }
}