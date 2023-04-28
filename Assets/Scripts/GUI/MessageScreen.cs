using TMPro;
using UnityEngine;

public class MessageScreen : MonoBehaviour
{
    private const string HIDE_TIMER = "Hide";

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
        PlayerEvents.current.onDeath += () => ShowMessage(gameOverMessage);
        GameEvents.current.onVictory += () => ShowMessage(victoryMessage);
    }

    private void ShowMessage(MessageCongif config){
        message.text = config.message;
        message.color = config.color;
        Show(config);
        HideAfterSomeTime(config);
    }

    private void Show(MessageCongif config){
        int animation = MessageAnimationsProvider.Get(config.appearAnimation);
        animator.Play(animation);
    }

    private void HideAfterSomeTime(MessageCongif config){
        timer.Set(HIDE_TIMER, config.time, () => Hide(config));
    }

    private void Hide(MessageCongif config){
        int animation = MessageAnimationsProvider.Get(config.disappearAnimation);
        animator.Play(animation);
    }
}
