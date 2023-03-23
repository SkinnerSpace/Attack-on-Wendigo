using System;
using System.Collections;
using UnityEngine;

public class HelicopterDoor : MonoBehaviour, IHelicopterDoor
{
    [SerializeField] private HelicopterDoorData data;
    [SerializeField] private Transform doorModel;

    private event Action onComplete;
    private event Action onOpened;
    private event Action onClosed;
    
    public void Subscribe(IHelicopterDoorObserver observer)
    {
        onOpened += observer.OnDoorHasOpened;
        onClosed += observer.OnDoorHasClosed;
    }

    public void Open() => StartCoroutine(SlideTheDoor(SlideUp));
    public void Close() => StartCoroutine(SlideTheDoor(SlideDown));

    private IEnumerator SlideTheDoor(Action slide)
    {
        data.SetStartTime(Time.time);
        onComplete = GetFinalAction(slide);

        while (data.currentTime < data.SlideTime)
        {
            CountDown();
            slide();

            yield return null;
        }

        onComplete();
    }

    private Action GetFinalAction(Action slide) => (slide == SlideUp) ? onOpened : onClosed;

    private void CountDown()
    {
        data.currentTime = Time.time - data.startTime;
        data.slidePhase = (data.currentTime / data.SlideTime).Clamp01();
    }

    private void SlideUp()
    {
        float scale = 1f - data.slidePhase.QuadEaseOut();
        ApplyScale(scale);
    }

    private void SlideDown()
    {
        float scale = data.slidePhase.QuadEaseInOut();
        ApplyScale(scale);
    }

    private void ApplyScale(float scale) => doorModel.localScale = new Vector3(1f, scale, 1f);
}
