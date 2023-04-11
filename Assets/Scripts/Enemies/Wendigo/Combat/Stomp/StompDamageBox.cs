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
        Vector3 impact = (other.transform.position - anchorPoint.position).FlatV3();
        impact += new Vector3(0f, 10f, 0f);
        impact = impact.normalized * stompDamage.impact;

        IHitBox hitBox = other.GetComponent<IHitBox>();
        DamagePackage damage = new DamagePackage(stompDamage.damage, impact, anchorPoint.position);
        hitBox.ReceiveDamage(damage);
    }
}
