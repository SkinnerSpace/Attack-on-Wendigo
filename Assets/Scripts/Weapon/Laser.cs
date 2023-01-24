using UnityEngine;

public class Laser : MonoBehaviour, IShooter, IVisionUser
{
    [SerializeField] private Transform shootPoint;
    private LineRenderer line;
    private PlayerVision vision;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void Shoot(bool isFiring) => EmitLaser();

    private void EmitLaser()
    {
        if (vision.hasTarget)
        {
            line.SetPosition(0, shootPoint.position);
            line.SetPosition(1, vision.Spot.point);
        }
    }
    public void ConnectVision(PlayerVision vision) => this.vision = vision;
}
