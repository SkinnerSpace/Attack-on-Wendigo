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

    [Header("Additional")]
    [SerializeField] private bool isDisposable;
    [SerializeField] private bool checkSourcePermeability;

    private float time;
    private bool isOnFire;
    private bool isActive = true;

    private event Action onFire;
    private event Action putOutTheFire;

    public void Subscribe(IInflammableObserver observer)
    {
        onFire += observer.SetOnFire;
        putOutTheFire += observer.CoolDown;
    }

    public void SetActive(bool isActive)
    {
        this.isActive = isActive;
        enabled = isActive;
        boxCollider.enabled = isActive;
    }

    public void InflameDirectly(Vector3 flamePoint)
    {
        if (checkSourcePermeability)
        {
            if (NoObstalces(flamePoint)){
                SetOnFire();
            }
        }
        else
        {
            SetOnFire();
        }
    }

    private bool NoObstalces(Vector3 flamePoint)
    {
        Vector3 flameDirection = (transform.position - flamePoint).normalized;

        if (Physics.Raycast(flamePoint, flameDirection, Mathf.Infinity, ComplexLayers.Ground)){
            return false;
        }

        return true;
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

            if (!isDisposable){
                EnableCollisionOnTimeOut();
            }

            PutOutTheFireOnTimeOut();
        }
    }

    private void EnableCollisionOnTimeOut()
    {
        if (time >= enableCollisionTime && !boxCollider.enabled)
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
