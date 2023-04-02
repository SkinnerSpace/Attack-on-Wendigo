using UnityEngine;

[ExecuteAlways]
public class GUIAnimator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AnimationCurveData data;
    [SerializeField] private RectTransform subject;

    [Header("Test")]
    [SerializeField] private bool testMode;
    [Range(0f, 1f)]
    [SerializeField] private float completeness;

    private float frame => testMode ? completeness * data.animationTime : time;
    private bool isPlaying = false;
    private float time;

    private void Update()
    {
        if (data != null)
        {
            if (isPlaying)
            {
                Tick();
            }

            UpdatePush();
        }
    }

    private void Tick()
    {
        if (time < data.animationTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            isPlaying = false;
        }

        completeness = time / data.animationTime;
    }

    public void PlayPush()
    {
        time = 0f;
        isPlaying = true;
    }

    private void UpdatePush()
    {
        float scale = data.animationCurve.Evaluate(frame);
        subject.localScale = new Vector3(scale, scale, 1f);
    }
}

