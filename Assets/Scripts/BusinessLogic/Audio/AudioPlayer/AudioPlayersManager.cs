using System;
using UnityEngine;

public class AudioPlayersManager : MonoBehaviour
{
    public static AudioPlayersManager Instance;

    public event Action<float> onTimeUpdate;

    private void Awake(){
        Instance = this;
    }

    private void Update(){
        onTimeUpdate?.Invoke(Time.realtimeSinceStartup);
    }

    public void SubscribeOnTimeUpdate(Action<float> onTimeUpdate) => this.onTimeUpdate += onTimeUpdate;
    public void UnsubscribeFromTimeUpdate(Action<float> onTimeUpdate) => this.onTimeUpdate -= onTimeUpdate;
}