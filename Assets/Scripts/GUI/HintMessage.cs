using TMPro;
using UnityEngine;

public class HintMessage : MonoBehaviour
{
    [SerializeField] private float moveDownTime = 1f;
    [SerializeField] private Vector3 defaultPosition;
    [SerializeField] private Vector3 targetPosition;

    private TextMeshProUGUI message;
    private GUIContainer container;

    private bool isMovingDown;
    private float time;
    private float transitionValue;

    private void Awake()
    {
        message = GetComponentInChildren<TextMeshProUGUI>();
        container = GetComponentInChildren<GUIContainer>();

        container.onAppeared += MoveDown;
    }

    private void Update()
    {
        if (isMovingDown){
            time += Time.deltaTime;

            if (time >= moveDownTime){
                isMovingDown = false;
            }

            transitionValue = Mathf.InverseLerp(0f, moveDownTime, time);
            transform.position = Vector3.Lerp(defaultPosition, targetPosition, transitionValue);
        }
    }

    public void SetMessage(string text)
    {
        message.text = text;
        container.ShowGradually();
        transform.position = defaultPosition;
    }

    private void MoveDown()
    {
        isMovingDown = true;
        time = 0f;
        transitionValue = 0f;
    }
}
