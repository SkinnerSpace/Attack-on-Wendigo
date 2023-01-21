using UnityEngine;

public class EnergyShieldSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;

    public void Spawn(Vector3 position)
    {
        Instantiate(shieldPrefab, position, Quaternion.identity, transform);
    }
}
