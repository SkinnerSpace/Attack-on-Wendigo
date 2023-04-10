using System.Collections.Generic;
using UnityEngine;

public class LimbsToPooledObjectConnector : MonoBehaviour
{
    [SerializeField] private WendigoPooledObject pooledObject;
    [SerializeField] private LimbsManager limbsManager;
    [SerializeField] private HitBoxManager hitBoxManager;
    [SerializeField] private PropDestroyerManager propDestroyerManager;
    [SerializeField] private CorpseCollisionController collisionController;

    [SerializeField] private Transform head;
    [SerializeField] private List<MeshRenderer> objectsToShow;

    private void Awake()
    {
        pooledObject.SubscribeOnSpawn(limbsManager.ResetState);
        pooledObject.SubscribeOnSpawn(hitBoxManager.ResetState);
        pooledObject.SubscribeOnSpawn(propDestroyerManager.ResetState);
        pooledObject.SubscribeOnSpawn(collisionController.SwitchOn);

        pooledObject.SubscribeOnSpawn(ShowHiddenObjects);
        pooledObject.SubscribeOnSpawn(ResetPositionOfSomeObjects);
    }

    private void ShowHiddenObjects()
    {
        foreach (MeshRenderer mesh in objectsToShow){
            mesh.enabled = true;
        }
    }

    private void ResetPositionOfSomeObjects()
    {
        head.localPosition = Vector3.zero;
        head.localEulerAngles = Vector3.zero;
    }
}
