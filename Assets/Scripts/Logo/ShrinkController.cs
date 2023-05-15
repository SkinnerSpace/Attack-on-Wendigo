using UnityEditor;
using UnityEngine;

public class ShrinkController : MonoBehaviour
{
    private static (float start, float current) time;

    private enum States
    {
        Idle,
        Shrink,
        Expand
    }

    [SerializeField] private EyesController eyesController;
    [SerializeField] private RectTransform shrinkContainer;

    [Header("Settings")]
    [SerializeField] private float shrinkTime;
    [SerializeField] private float expandTIme;

    [Header("Animation")]
    [SerializeField] private AnimationCurve shrinkCurve;
    [SerializeField] private AnimationCurve expandCurve;

    private States state;

    public void Play()
    {
        state = States.Shrink;
        ResetTimer();
    }

    public void Stop()
    {
        state = States.Idle;
        ResetTimer();
        shrinkContainer.localScale = Vector3.one;
    }

    private void Update()
    {
        switch (state)
        {
            case States.Shrink:
                UpdateShrinking();
                break;

            case States.Expand:
                UpdateExpansion();
                break;
        }
    }

    private void UpdateShrinking()
    {
        time.current = Time.realtimeSinceStartup - time.start;
        time.current = Mathf.Clamp(time.current, 0f, shrinkTime);

        float progress = Mathf.InverseLerp(0f, shrinkTime, time.current);
        Vector3 scale = shrinkCurve.Evaluate(progress).ToVector3();
        shrinkContainer.localScale = scale;

        eyesController.SetDilation(1f - progress);

        if (time.current >= shrinkTime)
        {
            ResetTimer();
            state = States.Expand;
        }
    }

    private void UpdateExpansion()
    {
        time.current = Time.realtimeSinceStartup - time.start;

        float progress = Mathf.InverseLerp(0f, expandTIme, time.current);
        Vector3 scale = expandCurve.Evaluate(progress).ToVector3();
        shrinkContainer.localScale = scale;

        eyesController.SetDilation(progress);

        if (time.current >= expandTIme)
        {
            ResetTimer();
            state = States.Idle;
        }
    }

    private void ResetTimer()
    {
        time.current = 0f;
        time.start = Time.realtimeSinceStartup;
    }
}