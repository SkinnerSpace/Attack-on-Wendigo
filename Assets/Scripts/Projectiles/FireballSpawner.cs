using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fireaball;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SpawnFireball();
        }
    }

    private void SpawnFireball()
    {
        Instantiate(fireaball, transform.position, Quaternion.identity);
    }
}
