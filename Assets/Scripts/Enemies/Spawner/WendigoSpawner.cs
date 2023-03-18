using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WendigoSpawner : MonoBehaviour
{
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

    [Header("Settings")]
    [SerializeField] private int maxCount;
    [SerializeField] private int concurrentCount;
    [SerializeField] private float minInterval = 5f;
    [SerializeField] private float maxInterval = 15f;

    private int wendigoCount = 0;

    private IObjectPooler pooler;
    private List<Transform> wendigos = new List<Transform>();

    private ICharacterData character;

    private void Awake()
    {
        counter.SubscribeOnTimeOut(Spawn);
        character = characterImp.GetComponent<ICharacterData>();
        //Pickable.SubscribeOnFirstPickUp(Spawn);
    }

    private void Start() => pooler = PoolHolder.Instance;

    public void Spawn()
    {
        Vector3 position = radPosGenerator.CalculatePos(wendigos);

        InstantiateWendigo(position);
        shieldSpawner.Spawn(position);
        lightningThrower.Throw(position);

        float density = wendigoCount / (float)concurrentCount;
        float interval = Mathf.Lerp(minInterval, maxInterval, density);

        if (wendigoCount < concurrentCount)
            timer.Set("Spawn", interval, Spawn);
    }

    private void InstantiateWendigo(Vector3 position)
    {
        wendigoCount += 1;

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

    }
}
