using UnityEngine;

public class GunBarrel : MonoBehaviour
{
    [SerializeField] private float shootInterval = 1f;
    public bool isReady { get; private set; } = true;

    public GameObject bullet { get; set; }
    private FunctionTimer timer;
    private GunSight gunSight;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
        gunSight = GetComponent<GunSight>();
    }

    public void Shoot()
    {
        isReady = false;
        timer.Set("Cool down", shootInterval, CoolDown);

        PlayShootVFX();
    }

    private void CoolDown()
    {
        isReady = true;
    }

    private void PlayShootVFX()
    {
        if (gunSight.Hit.point != Vector3.zero)
        {
            Instantiate(bullet, gunSight.Hit.point, Quaternion.identity);
        }
    }
}

