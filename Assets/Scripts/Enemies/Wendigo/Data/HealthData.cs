using System;
using UnityEngine;

namespace WendigoCharacter
{
    [Serializable]
    public class HealthData : IRebootable
    {
        public int Amount;
        public bool IsAlive = true;

        private int init_Amount;

        public void Save(){
            init_Amount = Amount;
        }

        public void Reboot(){
            Amount = init_Amount;
            IsAlive = true;
        }
    }
}
