using System;
using UnityEngine;

public class SnowTransitionManager : MonoBehaviour
{
    private SnowTransitionScreen transitionScreen;

    private void Awake(){
        transitionScreen = GetComponent<SnowTransitionScreen>();
    }

    private void Start()
    {
        GameEvents.current.onStart += Set(() => GameEvents.current.IntroIsOver());
        HelicopterEvents.current.onBoarded += Set(() => HelicopterEvents.current.NotifyOnIsGoingToSetOff());
    }

    private Action Set(Action action)
    {
        return () => transitionScreen.SetOnTransitionAction(action);
    }
}