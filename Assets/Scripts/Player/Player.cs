﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Look look; public Look Look => look;
    [SerializeField] private Walk walk; public Walk Walk => walk;
    [SerializeField] private Jump jump; public Jump Jump => jump;
    [SerializeField] private Fall fall; public Fall Fall => fall;
    [SerializeField] private Move move; public Move Move => move;

    public ITransformProxy transformProxy;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        transformProxy = new TransformProxy(transform);
    }

    private void Update()
    {
        Look.Execute();
        Walk.Execute();
        Jump.Execute();
        Fall.Execute();
        Move.Execute();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
