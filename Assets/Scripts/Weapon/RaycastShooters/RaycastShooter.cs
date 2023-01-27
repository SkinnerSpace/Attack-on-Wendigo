using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooter : MonoBehaviour, IShooter, IVisionUser
{
    [Header("Required Components")]
    [SerializeField] private WeaponRecoilController recoilController;

    [Header("Prefabs")]
    [SerializeField] private GameObject bulletExplosion;

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
        if (isFiring && isReady && vision.hasTarget)
        {
            isReady = false;
            spot = vision.Spot;
            Instantiate(bulletExplosion, spot.point, Quaternion.identity);
            recoilController.Recoil();

            timer.Set("CoolDown", coolDownTime, CoolDown);
        }
    }

    private void CoolDown() => isReady = true;

    public void ConnectVision(PlayerVision vision) => this.vision = vision;
}
