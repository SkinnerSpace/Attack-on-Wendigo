using System;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private event Action<Vector3> onForceUpdate;

    public void SubscribeOnForceUpdate(Action<Vector3> onForceUpdate) => this.onForceUpdate += onForceUpdate;

    public void ApplyForce(Vector3 force){
        onForceUpdate?.Invoke(force);
    }
}