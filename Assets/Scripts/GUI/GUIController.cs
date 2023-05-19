using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GUIContainer main;

    [SerializeField] private GUIContainer hUD;
    [SerializeField] private GUIContainer aim;
    [SerializeField] private GUIContainer uGUI;

    private bool playerIsActive;

    public bool mainIsHidden { get; private set; }

    private void Start()
    {
        ConnectMain();
        ConnectHUD();
    }

    private void ConnectMain()
    {
        GameEvents.current.onIntroIsOver += main.ShowGradually;
        GameEvents.current.onIntroIsOver += () => playerIsActive = true;

        GameEvents.current.onPause += () => 
        {
            if (playerIsActive){
                HideMainImmediately();
            }
        };

        GameEvents.current.onResume += () =>
        {
            if (playerIsActive){
                ShowMainImmediately();
            }
        };

        PlayerEvents.current.onDeath += HideAllButMain;
        HelicopterEvents.current.onBoarded += HideAllButMain;
    }

    private void ConnectHUD()
    {
        GameEvents.current.onVictory += hUD.HideGradually;
    }

    private void HideAllButMain()
    {
        hUD.HideGradually();
        aim.HideGradually();
        uGUI.HideGradually();
    }

    public void HideMainImmediately()
    {
        mainIsHidden = true;
        main.HideImmediately();
    }

    public void ShowMainImmediately()
    {
        mainIsHidden = true;
        main.ShowImmediately();
    }
}
