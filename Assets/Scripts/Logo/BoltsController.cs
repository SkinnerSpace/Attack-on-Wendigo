using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BoltsController : MonoBehaviour
{
    [Header("Next stage")]
    [SerializeField] private LogoPulseController pulseController;
    [SerializeField] private LogoFaceController faceController;
    [SerializeField] private ShrinkController shrinkController;

    [Header("Settings")]
    [SerializeField] private Vector2 destination;
    [SerializeField] private float piercingTime;
    [SerializeField] private float piercedTime;
    [SerializeField] private AnimationCurve curve;

    [Header("Holes Settings")]
    [SerializeField] private float holesStartLerp;
    [SerializeField] private float holesEndLerp;

    [Header("Bolts")]
    [SerializeField] private RectTransform leftBolt;
    [SerializeField] private RectTransform rightBolt;

    [Header("Images")]
    [SerializeField] private Image[] unnailedBolts;
    [SerializeField] private Image[] nailedBolts;

    [Header("Holes")]
    [SerializeField] private RectTransform leftHole;
    [SerializeField] private RectTransform rightHole;

    private static (float current, float start) time;

    private static bool isPlaying;
    private static bool isPierced;

    public void Play()
    {
        time.start = Time.realtimeSinceStartup;
        time.current = 0f;

        SetUnnailedImages();

        isPierced = false;
        isPlaying = true;
    }

    public void Stop()
    {
        time.current = 0f;
        isPlaying = false;
        isPierced = false;

        SetUnnailedImages();

        UpdatePiercing();
    }

    private void Update()
    {
        if (isPlaying)
        {
            CountDown();
            UpdatePiercing();
        }
    }

    private void CountDown()
    {
        time.current = Time.realtimeSinceStartup - time.start;

        if (!isPierced && time.current >= piercedTime)
        {
            isPierced = true;
            OnPierced();
        }

        if (time.current >= piercingTime)
        {
            time.current = piercingTime;
            isPlaying = false;
        }
    }

    private void OnPierced()
    {
        SetNailedImages();
        faceController.SetStage(LogoAnimationStages.Pierced);
        shrinkController.Play();
        pulseController.Play();
    }

    private void SetNailedImages()
    {
        foreach (Image unnailedBolt in unnailedBolts)
        {
            unnailedBolt.enabled = false;
        }

        foreach (Image nailedBolt in nailedBolts)
        {
            nailedBolt.enabled = true;
        }
    }

    private void SetUnnailedImages()
    {
        foreach (Image unnailedBolt in unnailedBolts)
        {
            unnailedBolt.enabled = true;
        }

        foreach (Image nailedBolt in nailedBolts)
        {
            nailedBolt.enabled = false;
        }
    }


    private void UpdatePiercing()
    {
        float curvePosition = Mathf.InverseLerp(0f, piercingTime, time.current);
        float value = curve.Evaluate(curvePosition);
        Vector2 rightPosition = Vector2.LerpUnclamped(Vector2.zero, destination, value);
        Vector2 leftPosition = new Vector2(rightPosition.x * -1f, rightPosition.y);

        rightBolt.anchoredPosition = rightPosition;
        leftBolt.anchoredPosition = leftPosition;

        float holeLerp = Mathf.InverseLerp(holesStartLerp, holesEndLerp, value);
        holeLerp = Easing.QuadEaseInOut(holeLerp);

        leftHole.localScale = holeLerp.ToVector3();
        rightHole.localScale = holeLerp.ToVector3();
    }
}
