using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPushable
{
    [SerializeField] private Look look; public Look Look => look;
    [SerializeField] private Walk walk; public Walk Walk => walk;
    [SerializeField] private Jump jump; public Jump Jump => jump;
    [SerializeField] private Fall fall; public Fall Fall => fall;
    [SerializeField] private Move move; public Move Move => move;

    public ITransformProxy transformProxy;
    public Vector3 position => transform.position;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        transformProxy = new TransformProxy(transform);

        Blizzard.Instance.AddInsider(this);
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

    public void Push(Vector3 pushVelocity)
    {
        move.Push(pushVelocity);
    }
}
