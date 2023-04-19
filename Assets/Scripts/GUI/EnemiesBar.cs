using TMPro;
using UnityEngine;

public class EnemiesBar : MonoBehaviour
{
    [SerializeField] private GUIContainer container;
    [SerializeField] private Transform spawnerImp;
    [SerializeField] private TextMeshProUGUI label;

    private ISpawner spawner;

    private void Awake()
    {
        spawner = spawnerImp.GetComponent<ISpawner>();
    }

    private void Start()
    {
        spawner.SubscribeOnAliveCountUpdate(OnUpdate);
        GameEvents.current.onInvasionHasBegun += container.ShowGradually;
    }

    public void OnUpdate(int count){
        label.text = count.ToString();
    }
}