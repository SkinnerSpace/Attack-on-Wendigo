using UnityEngine;

public class LimbsToPooledObjectConnector : MonoBehaviour
{
    [SerializeField] private WendigoPooledObject pooledObject;
    [SerializeField] private LimbsManager limbsManager;
    [SerializeField] private HitBoxManager hitBoxManager;
    [SerializeField] private PropDestroyerManager propDestroyerManager;
    [SerializeField] private CorpseCollisionController collisionController;

    private void Awake()
    {
        pooledObject.SubscribeOnSpawn(limbsManager.ResetState);
        pooledObject.SubscribeOnSpawn(hitBoxManager.ResetState);
        pooledObject.SubscribeOnSpawn(propDestroyerManager.ResetState);
        pooledObject.SubscribeOnSpawn(collisionController.SwitchOn);
    }
}
