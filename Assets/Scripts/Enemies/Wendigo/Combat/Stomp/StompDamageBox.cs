using UnityEngine;
using UnityEditor;

public class StompDamageBox : MonoBehaviour
{
    [SerializeField] private WendigoStompDamage stompDamage;
    [SerializeField] private Transform anchorPoint;
    [SerializeField] private Collider stompCollider;
    [SerializeField] private FunctionTimer timer;

    public void Activate()
    {
        transform.position = anchorPoint.position;
        stompCollider.enabled = true;

        timer.Set("Deactivate" + transform.name, 0.5f, () => stompCollider.enabled = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IHitBox hitBox = other.GetComponent<IHitBox>();
        DamagePackage damage = new DamagePackage(stompDamage.damage);
        hitBox.ReceiveDamage(damage);
    }
}
