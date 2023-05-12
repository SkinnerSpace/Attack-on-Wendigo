using UnityEngine;

[ExecuteAlways]
public class BoltsController : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float time;
    [SerializeField] private Vector2 destination;
    [SerializeField] private AnimationCurve curve;

    [Header("Bolts")]
    [SerializeField] private RectTransform leftBolt;
    [SerializeField] private RectTransform rightBolt;

    private void Update()
    {
        float value = curve.Evaluate(time);
        Vector2 rightPosition = Vector2.LerpUnclamped(Vector2.zero, destination, value);
        Vector2 leftPosition = new Vector2(rightPosition.x * -1f, rightPosition.y);

        rightBolt.anchoredPosition = rightPosition;
        leftBolt.anchoredPosition = leftPosition;
    }
}

