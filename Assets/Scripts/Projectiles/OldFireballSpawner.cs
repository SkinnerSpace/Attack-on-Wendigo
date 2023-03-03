using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldFireballSpawner : MonoBehaviour
{
    private IObjectPooler pooler;

    [Range(0, 180)]
    [SerializeField] private float verticalAngle;
    [Range(0, 180)]
    [SerializeField] private float horizontalAngle;

    public float VerticalAngle => verticalAngle;
    public float HorizontalAngle => horizontalAngle;

    private void Awake()
    {
        
    }

    private void Start()
    {
        pooler = PoolHolder.Instance;
    }


    public void SpawnFireball()
    {
        pooler.SpawnFromThePool("Fireball", transform.position, transform.rotation);
    }
}
