using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponVFXController : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private WeaponRecoilController recoilController;
    [SerializeField] private VisualEffect muzzleFlash;
    [SerializeField] private ParticleSystem bulletShell;
    [SerializeField] private Animator animator;
    [SerializeField] private string hitExplosion;

    private IObjectPooler pooler;

    private void Start()
    {
        pooler = PoolHolder.Instance;
    }

    public void PlayShootVFX()
    {
        recoilController.Recoil();
        bulletShell.Play();
        muzzleFlash.Play();
    }

    public void Hit(WeaponTarget target)
    {
        Quaternion rotation = SurfaceHitMirror.ReflectRotation(target.hitDirection, target.normal);
        pooler.SpawnFromThePool(hitExplosion, target.hitPosition, rotation);
    }
}
