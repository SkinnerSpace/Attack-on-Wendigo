using UnityEngine;
using System;

namespace WendigoCharacter
{
    [Serializable]
    public class WendigoTarget
    {
        public Transform Target;
        public Vector3 Position => Target.position;
        public bool IsGrounded { get; set; }
        public bool Exist => Target != null;

        public void Set(Transform target) => Target = target;
    }
}
