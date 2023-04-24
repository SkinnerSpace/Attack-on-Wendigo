using TMPro;
using UnityEngine;

public class MessageScreen : MonoBehaviour
{
    private const string HIDE_TIMER = "Hide";

    private static int appearAnimation = Animator.StringToHash("Appear");
    private static int disappearAnimation = Animator.StringToHash("Disappear");

    [Header("Required Components")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private FunctionTimer timer;

    [Header("Data")]
    [SerializeField] private MessageCongif startMessage;
    [SerializeField] private MessageCongif gameOverMessage;
    [SerializeField] private MessageCongif victoryMessage;

    private void Start()
    {
        GameEvents.current.onFirstWeaponPickedUp += () => ShowMessage(startMessage);
        GameEvents.current.onPlayerHasDied += () => ShowMessage(gameOverMessage);
        GameEvents.current.onVictory += () => ShowMessage(victoryMessage);
    }

    private void ShowMessage(MessageCongif config){
        message.text = config.message;
        message.color = config.color;
        Show();
        HideAfterSomeTime(config.time);
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
