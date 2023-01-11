using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LightningSpawner : MonoBehaviour
{
    private RadPosGenerator radPosGenerator;
    private FunctionTimer timer;

    [SerializeField] private GameObject lightningBolt;

    private void Awake()
    {
        radPosGenerator = GetComponent<RadPosGenerator>();
        //timer = GetComponent<FunctionTimer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SpawnLightningBolt();
    }

    private void SpawnLightningBolt()
    {
        Vector3 spawnPos = radPosGenerator.GetPosition();
        Instantiate(lightningBolt, spawnPos, Quaternion.identity, transform);
    } 
}
