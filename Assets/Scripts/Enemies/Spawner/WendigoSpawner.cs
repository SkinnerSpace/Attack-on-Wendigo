using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WendigoSpawner : MonoBehaviour, ISpawner
{
    private const string SPAWN_TIMER = "Spawn";

    [Header("Required Components")]
    [SerializeField] private LightningThrower lightningThrower;
    [SerializeField] private EnergyShieldSpawner shieldSpawner;
    [SerializeField] private RadPosGenerator radPosGenerator;
    [SerializeField] private Transform characters;
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private Transform characterImp;

    [Header("Prefabs")]
    [SerializeField] private GameObject wendigoPrefab;

    [Header("Count")]
    [SerializeField] private int spawnCount;
    [SerializeField] private int minConcurrentCount;
    [SerializeField] private int maxConcurrentCount;

    [Header("Time interval")]
    [SerializeField] private float minTimeInterval = 5f;
    [SerializeField] private float maxTimeInterval = 15f;

    private int concurrentCount;
    private int wendigosLeft;

    private IObjectPooler pooler;
    private List<Transform> wendigos = new List<Transform>();

    private ICharacterData character;

    private event Action<int> onCountUpdate;

    private void Awake()
    {
        character = characterImp.GetComponent<ICharacterData>();
        //Pickable.SubscribeOnFirstPickUp(Spawn);

        concurrentCount = minConcurrentCount;
        wendigosLeft = spawnCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Spawn();
        }
    }

    private void Start(){
        pooler = PoolHolder.Instance;
        onCountUpdate?.Invoke(wendigosLeft);
    }

    public void Launch()
    {
        if (IsAllowedToSpawn())
        {
            Spawn();
            SetSpawnTimer();
        }
    }

    public void Spawn()
    {
        spawnCount -= 1;
        HandleSpawn();
        //SetSpawnTimer();
    }

    private void HandleSpawn()
    {
        Vector3 position = radPosGenerator.CalculatePos(wendigos);

        InstantiateWendigo(position);
        shieldSpawner.Spawn(position);
        lightningThrower.Throw(position);
    }


    private float GetSpawnTimeInterval(){
        float density = wendigos.Count / (float)concurrentCount;
        float interval = Mathf.Lerp(minTimeInterval, maxTimeInterval, density);

        return interval;
    }

    private void InstantiateWendigo(Vector3 position)
    {
        Transform wendigoImp = pooler.SpawnFromThePool("Wendigo").transform;
        wendigos.Add(wendigoImp);

        IWendigo wendigo = wendigoImp.GetComponent<IWendigo>();
        wendigo.SetTarget(characterImp);
        wendigo.SubscribeOnDeath(OnDeath);
        SetInPlace(wendigoImp, position);
    }

    private void SetInPlace(Transform wendigo, Vector3 position)
    {
        wendigo.SetParent(characters);
        wendigo.position = position;

        wendigo.LookAt(characterImp.transform);
        wendigo.eulerAngles = new Vector3(0f, wendigo.eulerAngles.y, 0f);
    }

    public void OnDeath(Transform wendigo)
    {
        wendigos.Remove(wendigo);
        
        if (IsAllowedToSpawn()){
            SetSpawnTimer();
        }

        wendigosLeft -= 1;
        onCountUpdate?.Invoke(wendigosLeft);
    }

    private bool IsAllowedToSpawn(){
        return spawnCount > 0 &&
               wendigos.Count < concurrentCount;
    }

    private void SetSpawnTimer()
    {
        if (!timer.TimerExist(SPAWN_TIMER)){
            float interval = GetSpawnTimeInterval();
            timer.Set("Spawn", interval, Launch);
        }
    }

    public void SubscribeOnCountUpdate(Action<int> onCountUpdate) => this.onCountUpdate += onCountUpdate;
}
