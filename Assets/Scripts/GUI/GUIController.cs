using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] private GUIContainer main;
    [SerializeField] private GUIContainer hUD;

    private void Start()
    {
        ConnectMain();
        ConnectHUD();
    }

    private void ConnectMain()
    {
        GameEvents.current.onStart += main.ShowGradually;
        GameEvents.current.onPause += main.HideImmediately;
        GameEvents.current.onResume += main.ShowImmediately;
        GameEvents.current.onPlayerHasDied += main.HideGradually;
    }

    private void ConnectHUD()
    {
        GameEvents.current.onVictory += hUD.HideGradually;
    }
}