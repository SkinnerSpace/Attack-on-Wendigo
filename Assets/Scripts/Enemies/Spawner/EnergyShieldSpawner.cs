using UnityEngine;

public class EnergyShieldSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;

    private IObjectPooler pooler;

    private void Start(){
        pooler = PoolHolder.Instance;
    }

    public void Spawn(Vector3 position)
    {
        pooler.SpawnFromThePool("EnergyShield", position, Quaternion.identity);
    }
}
