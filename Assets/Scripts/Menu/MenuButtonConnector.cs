using UnityEngine;

public class MenuButtonConnector : MonoBehaviour
{
    private MenuButton button;

    private void Awake(){
        button = GetComponent<MenuButton>();
    }

    public void Click() => button.Click();
    public void Select() => button.Select();
    public void Deselect() => button.Deselect();
}