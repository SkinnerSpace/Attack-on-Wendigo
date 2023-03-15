using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour, IMenu
{
    private Button[] buttons;

    [SerializeField] private List<Menu> menus;
    private Dictionary<string, List<Button>> Menus = new Dictionary<string, List<Button>>();

    private List<Button> requiredButtons = new List<Button>();
    private List<Button> redundantButtons = new List<Button>();

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        
        foreach (Menu menu in menus)
            Menus.Add(menu.name, menu.buttons);
    }

    public void Open(string name)
    {
        CursorManager.UnlockCursor();
        gameObject.SetActive(true);

        SortButtons(Menus[name]);
        EnableRequiredButtons();
        DisableRedundantButtons();
    }

    private void SortButtons(List<Button> menu)
    {
        requiredButtons.Clear();
        redundantButtons.Clear();

        foreach (Button button in buttons)
        {
            if (menu.Contains(button))
                requiredButtons.Add(button);
            else
                redundantButtons.Add(button);
        }
    }

    private void EnableRequiredButtons()
    {
        foreach (Button button in requiredButtons)
            button.gameObject.SetActive(true);
    }

    private void DisableRedundantButtons()
    {
        foreach (Button button in redundantButtons)
            button.gameObject.SetActive(false);
    }

    public void Close()
    {
        CursorManager.LockCursor();
        gameObject.SetActive(false);
    }
}
