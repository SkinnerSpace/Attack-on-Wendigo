using UnityEngine;
using WendigoCharacter;

public class WendigoAdditionalCollider : MonoBehaviour, ISwitchable
{
    [SerializeField] private WendigoPooledObject pooledObject;
    [SerializeField] private Wendigo wendigo;

    private Collider additionalCollider;

    private void Awake()
    {
        additionalCollider = GetComponent<Collider>();

        pooledObject.SubscribeOnSpawn(SwitchOn);
        pooledObject.SubscribeOnBackToPool(SwitchOff);

        wendigo.onDeath += SwitchOff;
    }

    public void SwitchOn()
    {
        additionalCollider.enabled = true;
    }

    public void SwitchOff()
    {
        additionalCollider.enabled = false;
    }
}
