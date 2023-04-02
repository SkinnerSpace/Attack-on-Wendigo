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
    [SerializeField] private InvasionCounter counter;
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

    private IObjectPooler pooler;
    private List<Transform> wendigos = new List<Transform>();

    private ICharacterData character;

    private event Action<int> onCountUpdate;

    private void Awake()
    {
        counter.SubscribeOnTimeOut(Spawn);
        character = characterImp.GetComponent<ICharacterData>();
        //Pickable.SubscribeOnFirstPickUp(Spawn);

        concurrentCount = minConcurrentCount;
    }

    private void Start(){
        pooler = PoolHolder.Instance;
        onCountUpdate?.Invoke(spawnCount);
    }

    public void Spawn()
    {
        if (IsAllowedToSpawn()){
            spawnCount -= 1;

            HandleSpawn();
            SetSpawnTimer();
        }

        onCountUpdate?.Invoke(spawnCount);
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

        onCountUpdate?.Invoke(spawnCount);
    }

    private bool IsAllowedToSpawn(){
        return spawnCount > 0 &&
               wendigos.Count < concurrentCount;
    }

    private void SetSpawnTimer()
    {
        if (!timer.TimerExist(SPAWN_TIMER)){
            float interval = GetSpawnTimeInterval();
            timer.Set("Spawn", interval, Spawn);
        }
    }

    public void SubscribeOnCountUpdate(Action<int> onCountUpdate) => this.onCountUpdate += onCountUpdate;
}

