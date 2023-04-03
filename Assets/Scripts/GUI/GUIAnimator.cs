using UnityEngine;

[ExecuteAlways]
public class GUIAnimator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private AnimationCurvesDataPack data;
    [SerializeField] private RectTransform subject;

    [Header("Test")]
    [SerializeField] private bool testMode;
    [Range(0f, 1f)]
    [SerializeField] private float completeness;
    [SerializeField] private AnimationCurveData testAnimation;

    private AnimationCurveData currentAnimation;

    private bool isPlaying = false;
    private float time;

    private void Update()
    {
        if (MustUpdateAnimation()){
            Tick();
            UpdateAnimation();
        }
        else if (MustUpdateTestAnimation()){
            UpdateTestAnimation();
        }
    }

    private bool MustUpdateAnimation()
    {
        return !testMode && 
               isPlaying && 
               currentAnimation != null;
    }

    private void Tick()
    {
        if (time < currentAnimation.animationTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            isPlaying = false;
        }
    }

    private void UpdateAnimation()
    {
        float scale = currentAnimation.animationCurve.Evaluate(time);
        subject.localScale = new Vector3(scale, scale, 1f);
    }

    private bool MustUpdateTestAnimation()
    {
        return testMode && 
               testAnimation != null;
    }

    private void UpdateTestAnimation()
    {
        float value = completeness * testAnimation.animationTime;
        float scale = testAnimation.animationCurve.Evaluate(value);
        subject.localScale = new Vector3(scale, scale, 1f);
    }

    public void PlayPush()
    {
        currentAnimation = data.pushAnimation;
        time = 0f;
        isPlaying = true;
    }
}

