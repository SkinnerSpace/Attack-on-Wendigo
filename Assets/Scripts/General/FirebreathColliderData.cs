using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class FirebreathColliderData
    {
        [Range(0, 360)]
        public float FOVDeg = 45;
        public float RadiusOuter = 10f;
        public float RadiusInner = 1f;
        public float DistanceOffset;
        public int CollidersLimit = 16;
    }
}

