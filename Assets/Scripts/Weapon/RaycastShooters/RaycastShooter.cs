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
            if (vision.hasTarget) vFXController.Hit(spot);

            timer.Set("CoolDown", coolDownTime, CoolDown);
        }
    }

    private void CoolDown() => isReady = true;

    public void ConnectVision(PlayerVision vision) => this.vision = vision;
    public Vector3 GetDirToTarget() => (spot.transform.position - shootPoint.position).normalized;
}

