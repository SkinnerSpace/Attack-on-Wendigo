using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoSpawner : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] private LightningThrower lightningThrower;
    [SerializeField] private EnergyShieldSpawner shieldSpawner;
    [SerializeField] private RadPosGenerator radPosGenerator;
    [SerializeField] private Transform characters;

    [Header("Prefabs")]
    [SerializeField] private GameObject wendigoPrefab;

    public void Launch()
    {
        Vector3 position = radPosGenerator.GetPosition();

        SpawnWendigo(position);
        shieldSpawner.Spawn(position);
        lightningThrower.Throw(position);
    }

    private void SpawnWendigo(Vector3 position)
    {
        Transform wendigo = CreateIn(position);
        TurnToTarget(wendigo);
    }

    private Transform CreateIn(Vector3 position) => Instantiate(wendigoPrefab, position, Quaternion.identity, characters).transform;

    private void TurnToTarget(Transform wendigo)
    {
        wendigo.LookAt(PlayerCharacter.Instance.transform);
        wendigo.eulerAngles = new Vector3(0f, wendigo.eulerAngles.y, 0f);
    }
}
