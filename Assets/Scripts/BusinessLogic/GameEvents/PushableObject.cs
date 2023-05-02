using System;
using UnityEngine;

public class PushableObject : MonoBehaviour, ISwitchable
{
    private bool isActive;
    private event Action<Vector3> onForceUpdate;

    public void SwitchOn() => isActive = true;

    public void SwitchOff() => isActive = false;

    public void SubscribeOnForceUpdate(Action<Vector3> onForceUpdate) => this.onForceUpdate += onForceUpdate;

    public void ApplyForce(Vector3 force){
        if (isActive){
            onForceUpdate?.Invoke(force);
        }
    }
}