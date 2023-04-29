using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GUIContainer main;
    [SerializeField] private GUIContainer hUD;
    [SerializeField] private GUIContainer aim;
    [SerializeField] private GUIContainer uGUI;

    private bool playerIsActive;

    private void Start()
    {
        ConnectMain();
        ConnectHUD();
    }

    private void ConnectMain()
    {
        GameEvents.current.onGameBegun += main.ShowGradually;
        GameEvents.current.onGameBegun += () => playerIsActive = true;

        GameEvents.current.onPause += () => 
        {
            if (playerIsActive){
                main.HideImmediately();
            }
        };

        GameEvents.current.onResume += () =>
        {
            if (playerIsActive){
                main.ShowImmediately();
            }
        };

        PlayerEvents.current.onDeath += HideAllButMain;
        GameEvents.current.onHelicopterIsGoingToSetOff += HideAllButMain;
    }

    private void HideAllButMain()
    {
        hUD.HideGradually();
        aim.HideGradually();
        uGUI.HideGradually();
    }

    private void ConnectHUD()
    {
        GameEvents.current.onVictory += hUD.HideGradually;
    }
}