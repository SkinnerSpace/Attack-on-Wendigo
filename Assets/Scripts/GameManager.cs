using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform titans;

    [SerializeField] private GameObject titanPrefab;
    [SerializeField] private float spawnRadius = 300f;

    private void Awake()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SpawnTitan();
    }

    /*
    private void TimeOut()
    {
        SpawnTitan();
    }
    */

    private void SpawnTitan()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Quaternion spawnRotation = GetSpawnRotation(spawnPosition);

        Instantiate(titanPrefab, spawnPosition, spawnRotation, titans);
    }

    private Vector3 GetSpawnPosition()
    {
        //Vector3 center = Town.Instance.transform.position;
        int degrees = Random.Range(0, 361);

        float x = spawnRadius * Mathf.Cos(degrees * Mathf.Deg2Rad);
        if (Mathf.Abs(x) < 0.01f) x = 0;

        float y = spawnRadius * Mathf.Sin(degrees * Mathf.Deg2Rad);
        if (Mathf.Abs(y) < 0.01f) y = 0;

        Vector3 spawnPosition = new Vector3(x, 0f, y);
        return spawnPosition;
    }

    private Quaternion GetSpawnRotation(Vector3 spawnPosition)
    {
        Vector3 dirToCenter = (Town.Instance.transform.position - spawnPosition).normalized;
        Debug.Log(dirToCenter);
        Quaternion spawnRotation = Quaternion.LookRotation(dirToCenter);

        return spawnRotation;
    }
}
