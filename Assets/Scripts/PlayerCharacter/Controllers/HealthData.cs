using System;

namespace Character
{
    [Serializable]
    public class HealthData
    {
        public int Amount;
        public bool IsAlive => Amount > 0;
    }
}
