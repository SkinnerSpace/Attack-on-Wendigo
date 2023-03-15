using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoTarget
    {
        public Vector3 Position => transform.position;
        public bool IsGrounded { get; set; }
        private Transform transform;

        public WendigoTarget(Transform transform) => this.transform = transform;
    }
}
