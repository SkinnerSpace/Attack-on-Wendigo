using UnityEngine;

public class WendigoSpawner : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private LightningThrower lightningThrower;
    [SerializeField] private EnergyShieldSpawner shieldSpawner;
    [SerializeField] private RadPosGenerator radPosGenerator;
    [SerializeField] private Transform characters;
    [SerializeField] private FunctionTimer timer;

    [Header("Prefabs")]
    [SerializeField] private GameObject wendigoPrefab;
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private int wendigoMaxCount;
    [SerializeField] private float spawnInterval;
    private int wendigoCount = 0;

    public void Spawn()
    {
        Vector3 position = radPosGenerator.GetPosition();

        InstantiateWendigo(position);
        shieldSpawner.Spawn(position);
        lightningThrower.Throw(position);

        if (wendigoCount < wendigoMaxCount)
            timer.Set("Spawn", spawnInterval, Spawn);
    }

    private void InstantiateWendigo(Vector3 position)
    {
        wendigoCount += 1;
        Transform wendigo = CreateIn(position);
        TurnToTarget(wendigo);
    }

    private Transform CreateIn(Vector3 position) => Instantiate(wendigoPrefab, position, Quaternion.identity, characters).transform;

    private void TurnToTarget(Transform wendigo)
    {
        wendigo.LookAt(target.transform);
        wendigo.eulerAngles = new Vector3(0f, wendigo.eulerAngles.y, 0f);
    }
}
