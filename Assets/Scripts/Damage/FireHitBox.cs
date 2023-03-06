using System;
using System.Collections;
using UnityEngine;

public class FireHitBox : MonoBehaviour, IInflammable
{
    [Header("Required components")]
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Chronos chronos;

    [Header("Settings")]
    [SerializeField] private float enableCollisionTime = 0.2f;
    [SerializeField] private float putOutTheFireTime = 0.3f;

    private float time;
    private bool isOnFire;

    private void Update()
    {
        if (isOnFire)
        {
            time += chronos.DeltaTime;
            EnableCollisionOnTimeOut();
            PutOutTheFireOnTimeOut();
        }
    }

    public void SetOnFire()
    {
        isOnFire = true;
        boxCollider.enabled = false;
        time = 0f;
    }

    private void EnableCollisionOnTimeOut()
    {
        if (time >= enableCollisionTime)
            boxCollider.enabled = true;
    }

    private void PutOutTheFireOnTimeOut()
    {
        if (time >= putOutTheFireTime)
            isOnFire = false;
    }
}

public class BurnHandler : BaseController
{
    private FireHitBox fireHitBox;

    public override void Connect()
    {
        throw new System.NotImplementedException();
    }

    public override void Disconnect()
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize(MainController main)
    {
        throw new System.NotImplementedException();
    }
}