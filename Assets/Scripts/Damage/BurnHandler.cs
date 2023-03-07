using System;

public class BurnHandler
{
    private const string SCORCH_TIMER = "Scorch";
    private FunctionTimer timer;

    private float scorchTime = 1f;
    private bool isOnFire;

    private Action onFire;
    private Action onScorch;
    private Action onCoolDown;

    public BurnHandler(FireHitBox fireHitBox, FunctionTimer timer, float scorchTime)
    {
        fireHitBox.Subscribe(SetOnFire, CoolDown);

        this.timer = timer;
        this.scorchTime = scorchTime;
    }

    public void Subscribe(IBurnObserver observer)
    {
        onCoolDown += observer.CoolDown;
        onScorch += observer.Scorch;
    }

    public void SubscribeOnFire(Action onFire) => this.onFire += onFire;
    public void SubscribeOnScorch(Action onScorch) => this.onScorch += onScorch;
    public void SubscribeOnCoolDown(Action onCoolDown) => this.onCoolDown += onCoolDown;

    private void SetOnFire()
    {
        if (!isOnFire)
        {
            onFire?.Invoke();
            isOnFire = true;

            Scorch();
        }
    }

    private void Scorch()
    {
        timer.Set(SCORCH_TIMER, scorchTime, Scorch);
        onScorch?.Invoke();
    }

    private void CoolDown()
    {
        isOnFire = false;
        timer.Stop(SCORCH_TIMER);
        onCoolDown?.Invoke();
    }
}
