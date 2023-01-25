using UnityEngine;

public class ProjectileEmitter : MonoBehaviour, IShooter
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private TrajectoryMarker trajectoryMarker;
    [SerializeField] private Charger charger;

    [SerializeField] private float coolDownTime = 0.3f;

    private IProjectile projectile;
    private FunctionTimer timer;

    private bool ready = true;

    private void Awake()
    {
        timer = GetComponent<FunctionTimer>();
    }

    public void Shoot(bool isFiring)
    {
        if (isFiring) Charge();
        else Discharge();
    }

    private void Charge()
    {
        charger.Charge();
        DrawTrajectory();
    }

    private void Discharge()
    {
        if (charger.power > 0f) EmitProjectile();
        charger.Discharge();
        CancelTrajectory();
    }

    private void EmitProjectile()
    {
        if (ready){
            ready = false;

            CreateProjectile().Launch(GetForce());
            timer.Set("Cool Down", coolDownTime, CoolDown);
        }
    }

    private Vector3 GetForce() => shootPoint.forward * charger.power;
    private IProjectile CreateProjectile() => Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation).transform.GetComponent<IProjectile>();
    private void DrawTrajectory() => trajectoryMarker.DrawTrajectory(shootPoint.position, GetForce(), Physics.gravity);
    private void CancelTrajectory() => trajectoryMarker.CancelTrajectory();
    private void CoolDown() => ready = true;
}
