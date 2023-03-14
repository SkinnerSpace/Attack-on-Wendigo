using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private WendigoSpawner wendigoSpawner;

    private void Start()
    {
        //EventN1();
    }

    private void EventN1()
    {
        Pickable.SubscribeOnFirstPickUp(wendigoSpawner.Spawn);
    }
}