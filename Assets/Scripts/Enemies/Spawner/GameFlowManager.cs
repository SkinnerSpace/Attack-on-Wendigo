using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    private WendigoSpawner wendigoSpawner;

    private void Awake()
    {
        
    }

    private void EventN1()
    {
        Pickable.SubscribeOnFirstPickUp(wendigoSpawner.Spawn);
    }
}