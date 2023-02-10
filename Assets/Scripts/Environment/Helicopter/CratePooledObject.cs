using UnityEngine;

public class CratePooledObject : MonoBehaviour, IPooledObject
{
    [SerializeField] private Crate crate;
    [SerializeField] private CrateLandingController landingController;
    [SerializeField] private LaserBeam laserBeam;

    public GameObject Object => gameObject;

    public void OnObjectSpawn()
    {
        crate.ResetPhysics();
        landingController.ResetLanding();
        laserBeam.SwitchOff();
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);
}
