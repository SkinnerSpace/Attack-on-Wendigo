using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [NonSerialized] public int buildingsCount;

    public Dictionary<PropTypes, List<IProp>> Props { get; private set; } = new Dictionary<PropTypes, List<IProp>>();

    public static Town Instance { get; private set; }

    private void Awake()
    {
        Singleton();
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

    public void AddProp(PropTypes type, IProp prop)
    {
        if (!Props.ContainsKey(type)) Props.Add(type, new List<IProp>());
        Props[type].Add(prop);
    }
}
