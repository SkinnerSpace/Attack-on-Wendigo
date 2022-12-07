using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    public IBuilding[] buildings { get; private set; }
    [NonSerialized] public int buildingsCount;

    public static Town Instance { get; private set; }

    private void Awake()
    {
        Singleton();
        //InitializeBuildings();
    }

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /*
    private void InitializeBuildings()
    {
        buildings = GetComponentsInChildren<IBuilding>();
        buildingsCount = buildings.Length;

        foreach (Building building in buildings)
            building.SetTown(this);

        Debug.Log("Buildings " + buildingsCount);
    }
    */
}
