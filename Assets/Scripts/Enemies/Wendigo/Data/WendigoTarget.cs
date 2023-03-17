using UnityEngine;
using System;

namespace WendigoCharacter
{
    [Serializable]
    public class WendigoTarget
    {
        public Transform transform;
        public Vector3 Position => transform.position;
        public bool IsGrounded { get; set; }
        public bool Exist => transform != null;

        public void Set(Transform target) => transform = target;
    }
}
