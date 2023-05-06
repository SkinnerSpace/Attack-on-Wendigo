using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponVFXController : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private WeaponRecoilController recoilController;
    [SerializeField] private VisualEffect muzzleFlash;
    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] private string hitExplosion;

    private IObjectPooler pooler;

    private void Start()
    {
        pooler = PoolHolder.Instance;
        weapon.SubscribeOnShot(PlayShootVFX);
        weapon.SubscribeOnTargetIsShot(PlayHitTheSurfaceVFX);
    }

    private void PlayShootVFX()
    {
        recoilController.Recoil();
        muzzleFlash.Play();

        foreach (ParticleSystem particle in particles){
            particle.Play();
        }
    }

    private void PlayHitTheSurfaceVFX(WeaponTarget target)
    {
        Quaternion rotation = SurfaceHitMirror.ReflectRotation(target.hitDirection, target.normal);
        pooler.SpawnFromThePool(hitExplosion, target.hitPosition, rotation);
    }
}