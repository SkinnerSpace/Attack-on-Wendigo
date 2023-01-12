using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoSpawner : MonoBehaviour
{
    private RadPosGenerator radPosGenerator;

    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private GameObject wendigo;
    [SerializeField] private GameObject shield;

    [SerializeField] private Transform characters;

    private void Awake()
    {
        radPosGenerator = GetComponent<RadPosGenerator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            SpawnWendigo();
    }

    private void SpawnWendigo()
    {
        Vector3 spawnPos = radPosGenerator.GetPosition();
        Instantiate(lightningBolt, spawnPos, Quaternion.identity, transform);
        Instantiate(shield, spawnPos, Quaternion.identity, transform);

        Transform wendigoTf = Instantiate(wendigo, spawnPos, Quaternion.identity, characters).transform;
        wendigoTf.LookAt(PlayerCharacter.Instance.transform);
        wendigoTf.eulerAngles = new Vector3(0f, wendigoTf.eulerAngles.y, 0f);
    }
}
