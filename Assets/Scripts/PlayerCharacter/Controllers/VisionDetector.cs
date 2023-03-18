using System;
using UnityEngine;

namespace Character
{
    public class VisionDetector : BaseController, IMousePosObserver
    {
        private Camera cam;
        private Transform target;
        private IInputReader input;

        public event Action<VisionTarget> onTargetUpdate;
        public event Action<Transform> onTargetTfUpdate;

        public override void Initialize(PlayerCharacter main)
        {
            cam = main.OldData.Cam;
            input = main.InputReader;
        }

        public override void Connect() => input.Get<MousePositionInputReader>().Subscribe(this);
        public override void Disconnect() => input.Get<MousePositionInputReader>().Unsubscribe(this);

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
}