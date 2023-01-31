﻿using UnityEngine;
using UnityEngine.VFX;

public class WeaponVFXController : MonoBehaviour
{
    private static int shootAnim = Animator.StringToHash("Shoot");

    [Header("Required Components")]
    [SerializeField] private WeaponRecoilController recoilController;
    [SerializeField] private VisualEffect muzzleFlash;
    [SerializeField] private ParticleSystem bulletShell;
    [SerializeField] private Animator animator;

    [Header("Prefabs")]
    [SerializeField] private GameObject bulletExplosion;

    public void PlayShootVFX()
    {
        recoilController.Recoil();
        animator.Play(shootAnim);
        bulletShell.Play();
        muzzleFlash.Play();
    }

    public void Hit(WeaponTarget target)
    {
        Instantiate(bulletExplosion, target.hitPosition, Quaternion.identity);
    }
}

