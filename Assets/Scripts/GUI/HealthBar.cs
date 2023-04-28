using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    private GUIAnimator animator;

    private void Awake()
    {
        animator = GetComponent<GUIAnimator>();
    }

    private void Start(){
        PlayerEvents.current.onHealthUpdate += OnUpdate;
    }

    public void OnUpdate(int health){
        label.text = health.ToString();
        animator.PlayPush();
    }
}
