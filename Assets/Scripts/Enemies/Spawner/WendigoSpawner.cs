using UnityEngine;

public class WendigoSpawner : MonoBehaviour, ITimeOutObserver
{
    [Header("Required Components")]
    [SerializeField] private LightningThrower lightningThrower;
    [SerializeField] private EnergyShieldSpawner shieldSpawner;
    [SerializeField] private RadPosGenerator radPosGenerator;
    [SerializeField] private Transform characters;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private InvasionCounter counter;

    [Header("Prefabs")]
    [SerializeField] private GameObject wendigoPrefab;
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private int wendigoMaxCount;
    [SerializeField] private float spawnInterval;
    private int wendigoCount = 0;

    private IObjectPooler pooler;

    private void Awake() => counter.SubscribeOnTimeOut(this);
    private void Start() => pooler = PoolHolder.Instance;

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
        Transform wendigo = pooler.SpawnFromThePool("Wendigo").transform;
        wendigo.GetComponent<IWendigo>().SetTarget(target);
        SetInPlace(wendigo, position);
    }

    private void SetInPlace(Transform wendigo, Vector3 position)
    {
        wendigo.SetParent(characters);
        wendigo.position = position;

        wendigo.LookAt(target.transform);
        wendigo.eulerAngles = new Vector3(0f, wendigo.eulerAngles.y, 0f);
    }

    public void OnTimeOut() => Spawn();
}
