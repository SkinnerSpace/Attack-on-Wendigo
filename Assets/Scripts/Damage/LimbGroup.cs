using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LimbGroup : MonoBehaviour, IDamageable
{
    [SerializeField] private List<Limb> limbs;

    private HitBoxProxy hitBox;

    private void Awake()
    {
        hitBox = GetComponent<HitBoxProxy>();
        hitBox.Subscribe(this);
        hitBox.IncrementJoints(limbs.Count);

        foreach (Limb limb in limbs){
            limb.AddHitBox(hitBox);
        }
    }

    public void ReceiveDamage(DamagePackage damagePackage)
    {
        if (limbs.Count > 0){
            Limb targetLimb = limbs.OrderBy(limb => Vector3.Distance(limb.transform.position, damagePackage.point)).First();
            targetLimb.ReceiveDamage(damagePackage);
        }
    }
}
