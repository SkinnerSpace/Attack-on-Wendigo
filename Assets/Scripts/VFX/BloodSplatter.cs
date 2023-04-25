using UnityEngine;
using UnityEngine.UI;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField] private float minTime = 0.5f;
    [SerializeField] private float maxTime = 1.5f;

    private Image image;

    private float pattern;
    private float time;
    private float targetTime;
    private float alpha = 1f;

    private bool isBleeding = true;

    private void Awake()
    {
        image = GetComponent<Image>();
        targetTime = Rand.Range(minTime, maxTime);

        SetRandomPattern();
        UpdateShader();
    }

    private void SetRandomPattern(){
        pattern = Rand.Range01();
    }

    private void Update()
    {
        if (isBleeding){
            Dissolve();
            UpdateShader();
        }
    }

    private void Dissolve()
    {
        time += Time.deltaTime;
        alpha = 1f - Easing.QuadEaseIn(Mathf.InverseLerp(0f, targetTime, time));

        if (time >= targetTime){
            isBleeding = false;
        }
    }

    private void UpdateShader(){
        image.color = new Color(image.color.r, image.color.g, pattern, alpha);
    }
}