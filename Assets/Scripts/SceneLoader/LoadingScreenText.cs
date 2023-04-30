using UnityEngine;
using TMPro;

public class LoadingScreenText : MonoBehaviour
{
    private TextMeshProUGUI label;
    private CanvasGroup canvas;

    private void Awake()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        canvas = GetComponentInChildren<CanvasGroup>();
    }

    public void SetAlpha(float alpha) => canvas.alpha = alpha;

    public void SetLoadingText() => label.text = "Loading";

    public void SetEndingText() => label.text = "Thanks for playing!";
}
