using System;
using UnityEngine;

public class BurnHandler : IInflammableObserver
{
    private const string SCORCH_TIMER = "Scorch";
    private FunctionTimer timer;

    private float scorchTime = 1f;
    private bool isOnFire;

    private event Action onScorch;
    private event Action onCoolDown;

    public event Action<bool> onScorchDamage;

    public BurnHandler(FireHitBox fireHitBox, FunctionTimer timer, float scorchTime)
    {
        fireHitBox.Subscribe(this);

        this.timer = timer;
        this.scorchTime = scorchTime;
    }

    public void Subscribe(IBurnObserver observer)
    {
        onScorch += observer.Scorch;
        onCoolDown += observer.CoolDown;
    }

    public void SetOnFire()
    {
        if (!isOnFire)
        { 
            Scorch();
            isOnFire = true;
        }
    }

    private void Scorch()
    {
        timer.Set(SCORCH_TIMER, scorchTime, Scorch);
        onScorch?.Invoke();

        if (!isOnFire)
        {
            onScorchDamage?.Invoke(true);
        }
        else
        {
            onScorchDamage?.Invoke(false);
        }
    }

    public void CoolDown()
    {
        isOnFire = false;
        timer.Stop(SCORCH_TIMER);
        onCoolDown?.Invoke();
    }
}
