using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class LevelData
    {
        public int Progression;
        public AnimationCurve HealthProgression;
        public AnimationCurve SpeedProgression;
    }
}
