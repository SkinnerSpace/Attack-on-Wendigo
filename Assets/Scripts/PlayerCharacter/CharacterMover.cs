using UnityEngine;
using System;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private CharacterData data;
    [SerializeField] private Chronos chronos;

    private event Action onUpdate;

    public void Subscribe(IMoverObserver observer) => onUpdate += observer.Update;

    private void Update()
    {
        data.CameraRotation = Quaternion.Euler(data.CameraViewEuler + data.CameraTiltEuler);
        onUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        data.PreviousVerticalVelocity = data.VerticalVelocity;
        data.Velocity = new Vector3(data.FlatVelocity.x, data.VerticalVelocity, data.FlatVelocity.y);
        data.Controller.Move(data.Velocity * chronos.DeltaTime);
    }
}

