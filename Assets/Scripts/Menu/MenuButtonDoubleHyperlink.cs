using UnityEngine;

public class MenuButtonDoubleHyperlink : MenuButton
{
    [SerializeField] private string before;
    [SerializeField] private string after;

    private void Start()
    {
        MenuEvents.current.onStart += SwapSubMenu;
        onCommand = () => MenuEvents.current.EnterSubMenu(before);
    }

    private void SwapSubMenu()
    {
        onCommand = () => MenuEvents.current.EnterSubMenu(after);
    }
}
