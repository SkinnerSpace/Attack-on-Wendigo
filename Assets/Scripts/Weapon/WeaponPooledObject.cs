using UnityEngine;

public class WeaponPooledObject : MonoBehaviour, IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => gameObject;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Pickable pickable;
    [SerializeField] private WeaponPhysics physics;
    private IObjectPooler pooler;

    private void Start() => pooler = PoolHolder.Instance;

    public void OnObjectSpawn()
    {
        float randomAngle = Rand.Range(0f, 360f);
        Vector3 randomEuler = new Vector3(0f, randomAngle, 0f);
        Quaternion randomRotation = Quaternion.Euler(randomEuler);
        transform.rotation = randomRotation;
        
        pickable.SwitchOn();
        weapon.Reload();
        physics.SetLevitation(true);
    }

    public void SetActive(bool active) => gameObject.SetActive(active);

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) => transform.SetPositionAndRotation(position, rotation);

    public void BackToPool() => pooler.PutIntoThePool(this);
}

