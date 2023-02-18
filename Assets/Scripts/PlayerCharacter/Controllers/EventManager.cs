using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, GameEvent> gameEvents = new Dictionary<string, GameEvent>();

    public void Subscribe(IEventListener listener, string eventName)
    {
        CreateEventIfDoesNotExist(eventName);
        gameEvents[eventName].SubscribeOnStart(listener);
    }

    public void Unsubscribe(IEventListener listener, string eventName)
    {
        if (gameEvents.ContainsKey(eventName))
            gameEvents[eventName].SubscribeOnStart(listener);
    }

    public void ConnectTrigger(EventTrigger eventTrigger, string eventName)
    {
        CreateEventIfDoesNotExist(eventName);
        gameEvents[eventName].Connect(eventTrigger);
    }

    private void CreateEventIfDoesNotExist(string eventName)
    {
        if (!gameEvents.ContainsKey(eventName))
            gameEvents.Add(eventName, new GameEvent());
    }
}
