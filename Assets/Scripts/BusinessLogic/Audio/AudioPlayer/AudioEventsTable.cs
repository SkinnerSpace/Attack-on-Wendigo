using System.Collections.Generic;
using UnityEngine;

public class AudioEventsTable : MonoBehaviour
{
    [SerializeField] private bool isActive;

    public static AudioEventsTable Instance { get; private set; }
    public bool IsActive => isActive;
    public List<string> events;

    private void Awake()
    {
        Instance = this;
    }

    public void AddEvent(string audioEvent)
    {
        InitializeIfNecessary();
        events.Add(audioEvent);
    }

    public void RemoveEvent(string audioEvent)
    {
        InitializeIfNecessary();
        events.Remove(audioEvent);
    }

    private void InitializeIfNecessary()
    {
        if (events == null){
            events = new List<string>();
        }
    }
}
