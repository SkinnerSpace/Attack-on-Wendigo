using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GunSight gunSight;
    [SerializeField] private GunMagazine gunMagazine;
    [SerializeField] private GunBarrel gunBarrel;

    public Action notifyOutOfAmmo;

    private void Start()
    {
        notifyOutOfAmmo += AmmoBar.Instance.UpdateOutOfAmmo;
    }

    public void PullTheTrigger()
    {
        if (gunBarrel.isReady)
        {
            if (!gunMagazine.IsEmpty())
            {
                gunBarrel.bullet = gunMagazine.TakeAmmo();
                gunBarrel.Shoot();
                
                DamageTargetIfExist();
            }
            else
            {
                notifyOutOfAmmo();
            }
        }
    }

    public void Reload()
    {
        gunMagazine.Reload();
    }

    private void DamageTargetIfExist()
    {
        if (gunSight.Damageable != null)
        {
            DamagePackage damagePackage = new DamagePackage(1f);
            gunSight.Damageable.ReceiveDamage(damagePackage);
        }
    }
}

