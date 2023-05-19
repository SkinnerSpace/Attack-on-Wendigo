using UnityEngine;

public class MenuButtonDoubleHyperlink : MenuButton
{
    [SerializeField] private string before;
    [SerializeField] private string after;

    protected override void Awake()
    {
        base.Awake();

        onCommand = () =>
        {
            if (MenuEvents.current != null)
            {
                if (MenuEvents.current.gameHasBegun)
                {
                    MenuEvents.current.EnterSubMenu(after);
                }
                else
                {
                    MenuEvents.current.EnterSubMenu(before);
                }
            }
        };
    }
}
