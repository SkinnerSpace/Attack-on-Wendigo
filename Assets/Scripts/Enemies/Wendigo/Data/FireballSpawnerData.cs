using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class FireballSpawnerData
    {
        [Range(0, 180)]
        public float VerticalAngle;
        [Range(0, 180)]
        public float HorizontalAngle;
    }
}
