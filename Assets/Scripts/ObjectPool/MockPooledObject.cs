﻿using UnityEngine;

public class MockPooledObject : IPooledObject
{
    public string PoolTag { get; set; }
    public GameObject Object => throw new System.NotImplementedException();

    public void OnObjectSpawn() { }

    public void SetActive(bool active) { }

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation) { }
}