using TMPro;
using UnityEngine;

public class MessageScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI message;

    private void Start()
    {
        GameEvents.current.onFirstWeaponPickedUp += OnStart;
        GameEvents.current.onPlayerHasDied += OnGameOver;
        GameEvents.current.onWendigosAreDefeated += OnVictory;
    }

    private void OnStart(){
        message.text = "SURVIVE";
        Show();
    }

    private void OnGameOver(){
        message.text = "YOU DIED";
        Show();
    }

    private void OnVictory(){
        message.text = "VICTORY";
        Show();
    }

    private void Show(){
        canvasGroup.alpha = 1f;
    }
}