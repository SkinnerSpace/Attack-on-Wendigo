using TMPro;
using UnityEngine;

public class EnemiesBar : MonoBehaviour
{
    [SerializeField] private Transform spawnerImp;
    [SerializeField] private TextMeshProUGUI label;

    private ISpawner spawner;

    private void Awake()
    {
        spawner = spawnerImp.GetComponent<ISpawner>();
        spawner.SubscribeOnCountUpdate(OnUpdate);
    }

    public void OnUpdate(int count){
        label.text = count.ToString();
    }
}