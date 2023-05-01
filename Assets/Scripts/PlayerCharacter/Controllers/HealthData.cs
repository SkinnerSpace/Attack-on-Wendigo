using System;
using UnityEngine;

namespace Character
{
    [Serializable]
    public class HealthData
    {
        public int amount = 20;
        public int maxAmount = 20;
        public bool isImmortal;
    }
}
