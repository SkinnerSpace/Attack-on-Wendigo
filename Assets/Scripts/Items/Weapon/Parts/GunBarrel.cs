using UnityEngine;

public class GunBarrel : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootInterval = 1f;

    private float power;
    public bool isReady { get; private set; } = true;

    public GameObject projectile { get; set; }
    private FunctionTimer timer;
    private GunSight gunSight;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
        gunSight = GetComponent<GunSight>();
    }

    public void Load(GameObject projectile) => this.projectile = projectile;

    public void Shoot()
    {
        isReady = false;
        timer.Set("Cool down", shootInterval, CoolDown);

        IProjectile currentProjectile = Instantiate(projectile, shootPoint.position, shootPoint.rotation).GetComponent<IProjectile>();
        currentProjectile.Launch(shootPoint.forward * power);
    }

    private void CoolDown() => isReady = true;

    private void PlayVFX()
    {
        if (gunSight.Hit.point != Vector3.zero)
        {
            Instantiate(projectile, gunSight.Hit.point, Quaternion.identity);
        }
    }
}

