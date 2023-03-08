using System;

namespace WendigoCharacter
{
    [Serializable]
    public class HealthData
    {
        public int Amount;
        public bool IsAlive => Amount > 0;
    }
}
