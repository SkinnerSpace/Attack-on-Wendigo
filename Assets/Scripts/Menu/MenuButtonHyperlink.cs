using UnityEngine;

public class MenuButtonHyperlink : MenuButton
{
    [SerializeField] private string subMenuName;

    private void Start(){
        onCommand = () => MenuEvents.current.EnterSubMenu(subMenuName);
    }
}
