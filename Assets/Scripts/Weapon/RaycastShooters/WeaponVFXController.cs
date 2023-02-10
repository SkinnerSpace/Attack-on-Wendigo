﻿using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponVFXController : MonoBehaviour
{
    private static int shootAnim = Animator.StringToHash("Shoot");
    [SerializeField] private bool playAnimation;

    [Header("Required Components")]
    [SerializeField] private WeaponRecoilController recoilController;
    [SerializeField] private VisualEffect muzzleFlash;
    [SerializeField] private ParticleSystem bulletShell;
    [SerializeField] private Animator animator;

    private IObjectPooler pooler;

    private void Start()
    {
        pooler = PoolHolder.Instance;
    }

    public void PlayShootVFX()
    {
        recoilController.Recoil();
        if (playAnimation) animator.Play(shootAnim);
        bulletShell.Play();
        muzzleFlash.Play();
    }

    public void Hit(WeaponTarget target)
    {
        pooler.SpawnFromThePool("BulletExplosion", target.hitPosition, Quaternion.identity);
    }
}
