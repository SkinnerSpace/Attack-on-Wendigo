using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    private IObjectPooler pooler;

    private void Start()
    {
        pooler = PoolHolder.Instance;
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SpawnFireball();
        }
    }*/

    public void SpawnFireball()
    {
        pooler.SpawnFromThePool("Fireball", transform.position, transform.rotation);
    }
}
