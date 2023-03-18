using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private WendigoSpawner wendigoSpawner;

    [Header("Settings")]
    [SerializeField] private bool spawnWendigo;

    private void Start()
    {
        if (spawnWendigo)
        {
            EventN1();
        }
    }

    private void EventN1()
    {
        Pickable.SubscribeOnFirstPickUp(wendigoSpawner.Spawn);
    }
}