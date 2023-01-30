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

    private PlayerVision vision;
    private RaycastHit spot;
    private FunctionTimer timer;

    private float coolDownTime = 0.3f;
    private bool isReady = true;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
    }

    public void Shoot(bool isFiring)
    {
        if (isFiring && isReady)
        {
            isReady = false;
            spot = vision.Spot;

            sFXPlayer.PlayShootSFX();
            vFXController.Shoot();
            HitTheTarget();

            timer.Set("CoolDown", coolDownTime, CoolDown);
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

