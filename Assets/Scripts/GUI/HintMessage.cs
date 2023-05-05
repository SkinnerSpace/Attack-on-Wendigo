using TMPro;
using UnityEngine;

public class HintMessage : MonoBehaviour
{
    private const string MOVE_DOWN_TIMER = "MoveDown";
    private const string DISAPPEAR_TIMER = "Disappear";

    private enum States
    {
        Idle,
        Appearing,
        MovingDown
    }

    [Header("Required Components")]
    [SerializeField] private FunctionTimer timer;

    [Header("Time")]
    [SerializeField] private float moveDownTime = 1f;
    [SerializeField] private float centerTime = 1f;
    [SerializeField] private float downTime = 2f;

    [Header("Position")]
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Vector3 targetPosition;

    [Header("Scale")]
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 defaultScale;

    [Header("References")]
    [SerializeField] private FMODUnity.EventReference appearSFX;

    private RectTransform rectTransform;
    private TextMeshProUGUI message;
    private GUIContainer container;
    private AudioPlayer appearAudioPlayer;

    private float time;
    private float transitionValue;

    private States state;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        message = GetComponentInChildren<TextMeshProUGUI>();
        container = GetComponentInChildren<GUIContainer>();

        container.onAppeared += WaitAndMoveDown;
        container.onVisibilityUpdate += UpdateProgress;

        appearAudioPlayer = AudioPlayer.Create(appearSFX).WithPitch(-1f, 1f);
    }

    public void Show(IHint hint)
    {
        timer.Stop(MOVE_DOWN_TIMER);
        timer.Stop(DISAPPEAR_TIMER);

        message.text = hint.text;
        message.color = hint.color;
        rectTransform.localScale = minScale;

        state = States.Appearing;

        container.ShowGradually();
        rectTransform.anchoredPosition = defaultPosition;

        appearAudioPlayer.PlayOneShot();
    }

    private void WaitAndMoveDown()
    {
        state = States.Idle;
        timer.Set(MOVE_DOWN_TIMER, centerTime, MoveDown);
    }

    private void MoveDown()
    {
        state = States.MovingDown;

        time = 0f;
        transitionValue = 0f;
    }

    private void Update()
    {
        UpdateMovingDown();
    }

    private void UpdateMovingDown()
    {
        if (state == States.MovingDown)
        {
            CountDown();
            UpdatePosition();
        }
    }

    private void CountDown()
    {
        time += Time.deltaTime;

        if (time >= moveDownTime)
        {
            state = States.Idle;
            timer.Set(DISAPPEAR_TIMER, downTime, Hide);
        }
    }

    private void UpdatePosition()
    {
        transitionValue = Mathf.InverseLerp(0f, moveDownTime, time);
        transitionValue = Easing.QuadEaseInOut(transitionValue);
        rectTransform.anchoredPosition = Vector3.Lerp(defaultPosition, targetPosition, transitionValue);
    }

    public void Hide()
    {
        container.HideGradually();
    }

    private void UpdateProgress(float progress)
    {
        if (state == States.Appearing){
            rectTransform.localScale = Vector3.Lerp(minScale, defaultScale, progress);
        }
    }
}
