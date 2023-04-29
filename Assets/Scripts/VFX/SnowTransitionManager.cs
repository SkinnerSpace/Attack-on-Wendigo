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
        GameEvents.current.onStart += Set(() => GameEvents.current.TheGameHasBegun());
        GameEvents.current.onHelicopterOnBoard += Set(() => GameEvents.current.HelicopterIsGoingToSetOff());
    }

    private Action Set(Action action)
    {
        return () => transitionScreen.SetOnTransitionAction(action);
    }
}