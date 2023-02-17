using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Game Event", fileName="New Game Event")]
public class GameEvent : ScriptableObject
{
    private HashSet<GameEventListener> listeners = new HashSet<GameEventListener>();

    public void Invoke()
    {
        foreach (var globalEventListener in listeners)
            globalEventListener.RaiseEvent();
    }

    public void Subscribe(GameEventListener listener) => listeners.Add(listener);
    public void Unsubscribe(GameEventListener listener) => listeners.Remove(listener);
}
