using System;

namespace WendigoCharacter
{
    [Serializable]
    public class HealthData : IRebootable
    {
        public int Amount;
        public int InitialAmount;
        public float Percent => Amount / (float)InitialAmount;
        public bool IsAlive = true;

        private int init_Amount;
        private int init_InitialAmount;

        public void Save(){
            init_Amount = Amount;
            init_InitialAmount = InitialAmount;
        }

        public void Reboot(){
            Amount = init_Amount;
            InitialAmount = init_InitialAmount;
            IsAlive = true;
        }
    }
}
