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
    private bool isActive = true;

    private event Action onFire;
    private event Action putOutTheFire;

    public void Subscribe(Action onFire, Action putOutTheFire)
    {
        this.onFire += onFire;
        this.putOutTheFire += putOutTheFire;
    }

    public void Subscribe(Action onFire) => this.onFire += onFire;

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        enabled = isActive;
        boxCollider.enabled = isActive;
    }

    public void SetOnFire()
    {
        if (isActive)
        {
            isOnFire = true;
            boxCollider.enabled = false;
            time = 0f;

            onFire?.Invoke();
        }
    }

    private void Update()
    {
        if (isOnFire)
        {
            time += chronos.DeltaTime;
            EnableCollisionOnTimeOut();
            PutOutTheFireOnTimeOut();
        }
    }

    private void EnableCollisionOnTimeOut()
    {
        if (time >= enableCollisionTime)
            boxCollider.enabled = true;
    }

    private void PutOutTheFireOnTimeOut()
    {
        if (time >= putOutTheFireTime)
        {
            isOnFire = false;
            putOutTheFire?.Invoke();
        }
    }
}
