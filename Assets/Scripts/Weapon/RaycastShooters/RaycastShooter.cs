using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooter : MonoBehaviour, IShooter, IVisionUser
{
    private const float flyPower = 100f;

    [Header("Required Components")]
    [SerializeField] private WeaponVFXController vFXController;
    [SerializeField] private WeaponSFXPlayer sFXPlayer;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Magazine magazine;
    [SerializeField] private WeaponSight weaponSight;

    private PlayerVision vision;
    private RaycastHit spot;
    private FunctionTimer timer;

    private float precision;

    private float coolDownTime = 0.3f;
    private bool isReady = true;

    private bool wasFiring;
    private bool hadAmmo;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
    }

    public void Shoot(bool isFiring)
    {
        if (isFiring && isReady)
        {
            if (magazine.HasAmmo())
            {
                DoShoot();
            }
            else
            {
                ReactOnTheLackOfAmmo(isFiring);
            }
        }

        wasFiring = isFiring;
    }

    private void DoShoot()
    {
        ManageShootState();
        GetTheSpot();
        HitTheTarget();

        sFXPlayer.PlayShootSFX();
        vFXController.Shoot();

        weaponSight.GetTheSpot(precision);
        weaponSight.AimOffhand(precision);

        timer.Set("CoolDown", coolDownTime, CoolDown);
    }

    private void ManageShootState()
    {
        isReady = false;
        hadAmmo = true;
        magazine.ReduceCount();
    }

    private void GetTheSpot()
    {
        spot = vision.Spot;
    }

    private void ReactOnTheLackOfAmmo(bool isFiring)
    {
        if (!wasFiring && isFiring || hadAmmo)
        {
            hadAmmo = false;
            sFXPlayer.PlayIsEmptySFX();
            AmmoBar.Instance.UpdateOutOfAmmo();
        }
    }

    private void HitTheTarget()
    {
        if (vision.hasTarget)
        {
            BlowUpTheSurface();
            vFXController.Hit(spot);
        }
    }

    private void BlowUpTheSurface()
    {
        Surface surface = spot.transform.GetComponent<Surface>();
        if (surface != null)
        {
            surface.Hit().
                    WithPosition(spot.point).
                    WithAngle(GetDirToTarget(), spot.normal).
                    WithShape(0f, 45f).
                    WithCount(20, 30).
                    Launch();
        }
    }

    private void CoolDown() => isReady = true;
    public void ConnectVision(PlayerVision vision) => this.vision = vision;
    public Vector2 GetVelocity() => GetDirToTarget() * flyPower;
    public Vector3 GetDirToTarget() => (spot.point - vision.transform.position).normalized;
}

