using TMPro;
using UnityEngine;

public class MessageScreen : MonoBehaviour
{
    private const string HIDE_TIMER = "Hide";

    private static int appearAnimation = Animator.StringToHash("Appear");
    private static int disappearAnimation = Animator.StringToHash("Disappear");

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private FunctionTimer timer;

    private void Start()
    {
        GameEvents.current.onFirstWeaponPickedUp += OnStart;
        GameEvents.current.onPlayerHasDied += OnGameOver;
        GameEvents.current.onVictory += OnVictory;
    }

    public void OnStart(){
        message.text = "SURVIVE";
        Show();
        HideAfterSomeTime(2f);
    }

    public void OnGameOver(){
        message.text = "YOU DIED";
        Show();
        HideAfterSomeTime(3f);
    }

    public void OnVictory(){
        message.text = "VICTORY";
        Show();
        HideAfterSomeTime(2f);
    }

    private void Show(){
        animator.Play(appearAnimation);
    }

    private void HideAfterSomeTime(float hideTime){
        timer.Set(HIDE_TIMER, hideTime, Hide);
    }

    private void Hide(){
        animator.Play(disappearAnimation);
    }
}
