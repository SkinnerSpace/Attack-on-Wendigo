using UnityEngine;

public class CratePooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }

    [SerializeField] private Crate crate;
    [SerializeField] private CrateLandingController landingController;
    [SerializeField] private LaserBeam laserBeam;

    private IObjectPooler pooler;

    public GameObject Object => gameObject;

    private void Start() => pooler = PoolHolder.Instance;

    public void OnObjectSpawn()
    {
        crate.ResetState();
        landingController.ResetLanding();
        laserBeam.SwitchOff();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    public void BackToPool() => pooler.PutIntoThePool(this);
}
