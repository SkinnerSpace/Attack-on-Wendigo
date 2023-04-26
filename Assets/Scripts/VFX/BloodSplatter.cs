using System;
using UnityEngine;
using UnityEngine.UI;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField] private AnimationCurve scaleProgression;
    [SerializeField] private AnimationCurve alphaProgression;
    

    private Image image;
    private RectTransform rectTransform;

    private float time;
    private float targetTime;
    private float normalizedTime;

    private float pattern;

    private bool isBleeding;

    private Vector3 scale;
    private float alpha;

    public event Action onStopBleeding;

    private void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initialize(){
        SetRandomPattern();
        UpdateShader();
    }

    public void SetTime(float targetTime) => this.targetTime = targetTime;

    private void SetRandomPattern(){
        pattern = Rand.Range01();
    }

    public void Launch() => isBleeding = true;

    private void Update()
    {
        if (isBleeding) {
            Bleed();
        }
    }

    private void Bleed()
    {
        CountDown();

        scale = scaleProgression.Evaluate(normalizedTime).ToVector3();
        rectTransform.localScale = scale;

        alpha = alphaProgression.Evaluate(normalizedTime);
        UpdateShader();
    }

    private void CountDown(){

        time += Time.deltaTime;
        normalizedTime = Mathf.InverseLerp(0f, targetTime, time);

        if (time >= targetTime){
            isBleeding = false;
            onStopBleeding?.Invoke();
        }
    }

    private void UpdateShader(){
        image.color = new Color(image.color.r, image.color.g, pattern, alpha);
    }

    public void ResetState()
    {
        time = 0f;
        normalizedTime = 0f;
    }
}
