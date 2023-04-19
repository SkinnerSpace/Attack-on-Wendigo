using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WendigoSpawner : MonoBehaviour, ISpawner
{
    private const string SPAWN_TIMER = "Spawn";

    [Header("Required Components")]
    [SerializeField] private RadPosGenerator radPosGenerator;
    [SerializeField] private FunctionTimer timer;

    [SerializeField] private Transform container;
    [SerializeField] private Transform characterImp;

    [SerializeField] private LightningThrower lightningThrower;
    [SerializeField] private EnergyShieldSpawner shieldSpawner;

    [Header("Prefabs")]
    [SerializeField] private GameObject wendigoPrefab;

    [SerializeField] private WendigoSpawnConfig config;

    private WendigoSpawnerData data;
    private WendigoSpawnerLogic spawnerLogic;
    private WendigoManager wendigoManager;

    private bool isActive;

    private void Awake()
    {
        data = new WendigoSpawnerData(config);
        spawnerLogic = new WendigoSpawnerLogic(data, timer);
        spawnerLogic.SubscribeOnSpawn(Spawn);
        spawnerLogic.SubscribeOnSetSpawnTimer(SetSpawnTimer);
    }

    private void Start(){
        IObjectPooler pooler = PoolHolder.Instance;
        wendigoManager = new WendigoManager(spawnerLogic, container, characterImp, pooler);
        spawnerLogic.UpdateAliveCount();

        GameEvents.current.onFirstWeaponPickedUp += LaunchWithDelay;
    }

    public void LaunchWithDelay(){
        timer.Set("Launch", 2.6f, Launch);
    }

    public void Launch(){
        if (!isActive){
            isActive = true;
            spawnerLogic.SpawnIfPossible();
            GameEvents.current.InvasionHasBegun();
        }
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count-1; i++){
            Spawn();
        }
    }

    public void Spawn()
    {
        Vector3 position = radPosGenerator.CalculatePos(wendigoManager.Wendigos);

        wendigoManager.InstantiateWendigo(position, data);
        shieldSpawner.Spawn(position);
        lightningThrower.Throw(position);
    }

    private void SetSpawnTimer(float timeToSpawn)
    {
        if (IsAbleToSetTimer(timeToSpawn)){
             timer.Set(SPAWN_TIMER, timeToSpawn, spawnerLogic.SpawnIfPossible);
        }
    }

    private bool IsAbleToSetTimer(float timeToSpawn)
    {
        return !timer.TimerExist(SPAWN_TIMER) ||
               LessTimeLeftThanIsOffered(timeToSpawn);
    }

    private bool LessTimeLeftThanIsOffered(float timeToSpawn)
    {
        return timer.TimerExist(SPAWN_TIMER) &&
               timer.GetTimeLeft(SPAWN_TIMER) <= timeToSpawn;
    }

    public void SubscribeOnAliveCountUpdate(Action<int> onCountUpdate) => spawnerLogic.SubscribeOnAliveCountUpdate(onCountUpdate);
}
