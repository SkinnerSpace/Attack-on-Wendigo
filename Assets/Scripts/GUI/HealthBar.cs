using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public static HealthBar Instance;

    private void Awake(){
        Instance = this;
    }

    public void OnUpdate(int health)
    {
        label.text = health.ToString();
    }
}
