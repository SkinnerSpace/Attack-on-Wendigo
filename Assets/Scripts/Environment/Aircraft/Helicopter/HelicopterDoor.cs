using System;
using System.Collections;
using UnityEngine;

public class HelicopterDoor : MonoBehaviour, IHelicopterDoor
{
    [SerializeField] private HelicopterDoorData data;
    [SerializeField] private Transform doorModel;

    private HelicopterDoorSFXPlayer sFXPlayer;

    private event Action onComplete;
    private event Action onOpened;
    private event Action onClosed;

    private void Awake()
    {
        sFXPlayer = GetComponent<HelicopterDoorSFXPlayer>();
    }

    public void Subscribe(IHelicopterDoorObserver observer)
    {
        onOpened += observer.OnDoorHasOpened;
        onClosed += observer.OnDoorHasClosed;
    }

    public void Open() => StartCoroutine(SlideTheDoor(SlideUp));
    public void Close() => StartCoroutine(SlideTheDoor(SlideDown));

    public void OpenSilently() => StartCoroutine(SlideTheDoorSilently(SlideUp));
    public void CloseSilently() => StartCoroutine(SlideTheDoorSilently(SlideDown));

    private IEnumerator SlideTheDoor(Action slide)
    {
        data.SetStartTime(Time.time);
        onComplete = GetFinalAction(slide);
        PlaySFX(slide);

        while (data.currentTime < data.SlideTime)
        {
            CountDown();
            slide();

            yield return null;
        }

        onComplete();
    }

    private IEnumerator SlideTheDoorSilently(Action slide)
    {
        data.SetStartTime(Time.time);
        PlaySFX(slide);

        while (data.currentTime < data.SlideTime)
        {
            CountDown();
            slide();

            yield return null;
        }
    }

    private Action GetFinalAction(Action slide) => (slide == SlideUp) ? onOpened : onClosed;

    private void PlaySFX(Action slide)
    {
        if (slide == SlideUp){
            sFXPlayer.PlayDoorSlideUpSFX();
        }
        else{
            sFXPlayer.PlayDoorSlideDownSFX();
        }
    }

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
