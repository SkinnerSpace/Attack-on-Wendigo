using System.Collections.Generic;
using UnityEngine;

public class ParticleSoundConnector : MonoBehaviour
{
    [SerializeField] private List<ParticleSoundSystem> soundSystems;
    private IPooledObject pooledObject;

    private void Awake()
    {
        pooledObject = GetComponent<IPooledObject>();

        foreach (ParticleSoundSystem soundSystem in soundSystems){
            pooledObject.SubscribeOnSpawn(soundSystem.SwitchOn);
        }
    }
}
