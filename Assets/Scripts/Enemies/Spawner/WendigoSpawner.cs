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

        Transform wendigoTf = Instantiate(wendigo, spawnPos, Quaternion.identity, characters).transform;
        wendigoTf.LookAt(PlayerCharacter.Instance.transform);
        wendigoTf.eulerAngles = new Vector3(0f, wendigoTf.eulerAngles.y, 0f);

        Instantiate(shield, spawnPos, Quaternion.identity, transform);

        ThrowLightningBolt(spawnPos);
    }

    private void ThrowLightningBolt(Vector3 throwPos)
    {
        Instantiate(lightningBolt, throwPos, Quaternion.identity, transform);
        float dist = Vector3.Distance(throwPos, PlayerCharacter.Instance.transform.position);
        ScreenShake.Create().withTime(1f).WithAxis(1f, 1f, 0f).WithStrength(1f, 4f).WithCurve(10f, 0.1f, 0.25f).WithAttenuation(dist, 600f).Launch();
    }
}
