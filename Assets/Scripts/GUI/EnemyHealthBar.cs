using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;
    [SerializeField] private Image mask;
    [SerializeField] private TextMeshProUGUI label;

    private bool isActive;

    private void Awake(){
        UpdateValue(0f);
    }

    private void Start()
    {
        GameEvents.current.onEnemyHealthUpdate += UpdateValue;
    }

    public void UpdateValue(float value){
        mask.fillAmount = value;

        float health = Mathf.RoundToInt(value * 100f);
        label.text = health.ToString();

        if (health > 0){
            Activate();
        }
        else{
            Deactivate();
        }
    }

    private void Activate()
    {
        if (!isActive){
            isActive = true;
            canvas.alpha = 1f;
        }
    }

    private void Deactivate(){
        isActive = false;
        canvas.alpha = 0f;
    }
}
