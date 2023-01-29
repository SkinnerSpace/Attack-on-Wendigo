using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooter : MonoBehaviour, IShooter, IVisionUser
{
    [Header("Required Components")]
    [SerializeField] private WeaponVFXController vFXController;
    [SerializeField] private WeaponSFXPlayer sFXPlayer;
    [SerializeField] private Transform shootPoint;

    private PlayerVision vision;
    private RaycastHit spot;
    private FunctionTimer timer;

    private float coolDownTime = 0.3f;
    private bool isReady = true;

    Vector3 traceStart;
    Vector3 traceEnd;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
    }

    private void Update()
    {
        Debug.DrawLine(traceStart, traceEnd, Color.red);
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
            /*  Surface surface = spot.transform.GetComponent<Surface>();
              if (surface != null) Debug.Log(surface.Type);*/

            ISurface surface = spot.transform.GetComponent<ISurface>();
            if (surface != null)
            {
                Vector3 dir = GetDirToTarget();
                Vector3 hitVelocity = dir * 100f;
                surface.Hit(hitVelocity, 0f);

                traceStart = shootPoint.position;
                traceEnd = traceStart + dir * 1000f;
                Debug.Log($"Start {traceStart} end{traceEnd}");
            }

            vFXController.Hit(spot);
        }
    }

    private void CoolDown() => isReady = true;
    public void ConnectVision(PlayerVision vision) => this.vision = vision;
    public Vector3 GetDirToTarget() => (spot.point - shootPoint.position).normalized;
}

