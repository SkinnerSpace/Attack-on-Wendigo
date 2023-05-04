using System;
using UnityEngine;

public class GUIContainer : MonoBehaviour
{
    private enum Modes
    {
        Invisible,
        Visible,
        Appearing,
        Disappearing
    }

    private Modes mode = Modes.Invisible;

    private CanvasGroup canvasGroup;
    [SerializeField] private float appearTime = 1f;
    [SerializeField] private float disappearTime = 1f;

    private float time;
    private float visibility;

    public Action<float> onVisibilityUpdate;
    public Action onAppeared;
    public Action onDisappeared;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        switch (mode)
        {
            case Modes.Appearing:
                GraduallyAppear();
                break;

            case Modes.Disappearing:
                GraduallyDisappear();
                break;
        }
    }

    private void GraduallyAppear()
    {
        time += Time.unscaledDeltaTime;
        visibility = Mathf.InverseLerp(0f, appearTime, time);
        visibility = Easing.QuadEaseOut(visibility);
        SetAlpha(visibility);

        onVisibilityUpdate?.Invoke(visibility);

        if (visibility >= 1f){
            mode = Modes.Visible;
            onAppeared?.Invoke();
        }
    }

    private void GraduallyDisappear()
    {
        time += Time.unscaledDeltaTime;
        visibility = 1f - Mathf.InverseLerp(0f, disappearTime, time);
        visibility = Easing.QuadEaseOut(visibility);
        SetAlpha(visibility);

        onVisibilityUpdate?.Invoke(visibility);

        if (visibility <= Mathf.Epsilon){
            mode = Modes.Invisible;
            onDisappeared?.Invoke();
        }
    }

    public void ShowImmediately()
    {
        mode = Modes.Visible;
        SetAlpha(1f);
    }

    public void HideImmediately()
    {
        mode = Modes.Invisible;
        SetAlpha(0f);
    }

    public void ShowGradually()
    {
        time = 0f;
        mode = Modes.Appearing;
    }

    public void HideGradually()
    {
        time = 0f;
        mode = Modes.Disappearing;
    }

    private void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
