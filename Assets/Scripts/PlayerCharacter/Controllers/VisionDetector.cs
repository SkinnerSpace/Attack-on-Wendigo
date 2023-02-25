using System;
using UnityEngine;

public class VisionDetector : BaseController, IVisionDetector, IMousePosObserver
{
    private Camera cam;
    private Transform target;
    private IInputReader input;

    public event Action<VisionTarget> onTargetUpdate;
    public event Action<Transform> onTargetTfUpdate;

    public override void Initialize(MainController main)
    {
        cam = main.Data.Cam;
        input = main.InputReader;
    }

    public override void Connect() => input.Get<MousePositionInputReader>().Subscribe(this);

    public void Subscribe(Action<Transform> onTargetTfUpdate) => this.onTargetTfUpdate += onTargetTfUpdate;

    public void OnMousePosUpdate(Vector2 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        Transform currentTarget = null;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ComplexLayers.Interactables))
            currentTarget = hit.transform;

        if (target != currentTarget)
        {
            target = currentTarget;
            onTargetTfUpdate?.Invoke(target);
        }
    }
}

// Raycast with extension method
// Create a raycast class 
// Generalize PickUpController to interact with objects based on polymorphism
