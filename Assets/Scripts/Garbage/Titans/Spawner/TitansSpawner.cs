using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitansSpawner : MonoBehaviour
{
    private TitansLibrary library;
    [SerializeField] float spawnRadius;
    [SerializeField] int titansCount;

    private void Awake()
    {
        library = GetComponent<TitansLibrary>();
    }

    void Start()
    {
        for (int i = 0; i < titansCount; i++)
            SpawnTitan();
    }

    public void SpawnTitan()
    {
        Vector3 position = GetRandomPosition();
        Instantiate(library.GetTitan(0), position, Quaternion.identity, transform);
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 circlePos = Random.insideUnitCircle;
        Vector3 normalizedPos = new Vector3(circlePos.x, 0f, circlePos.y).normalized;
        Vector3 randomPos = (normalizedPos * spawnRadius) + transform.position;

        return randomPos;
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
    #endif
}
