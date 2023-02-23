using UnityEngine;
using System;

public class InvasionCounter : MonoBehaviour
{
    [SerializeField] private FunctionTimer timer;
    [SerializeField] private int countDownTime;
    private int time;

    private event Action<int> onTimeUpdate;
    private event Action onTimeOut;

    public void SubscribeOnUpdate(ITimeObserver observer) => onTimeUpdate += observer.OnTimeUpdate;
    public void SubscribeOnTimeOut(ITimeOutObserver observer) => onTimeOut += observer.OnTimeOut;

    public void Launch()
    {
        time = countDownTime;
        onTimeUpdate(time);

        WaitForASecond();
    }

    private void CountDown()
    {
        time -= 1;
        onTimeUpdate(time);

        if (time > 0) WaitForASecond();
        else onTimeOut?.Invoke();
    }

    private void WaitForASecond() => timer.Set("CountDown", 1f, CountDown);
}
