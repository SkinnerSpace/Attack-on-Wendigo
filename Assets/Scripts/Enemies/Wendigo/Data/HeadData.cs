using System;

namespace WendigoCharacter
{
    [Serializable]
    public class HeadData : IRebootable
    {
        public float LookAngleOfView;
        public bool OnTarget { get; set; }

        public void Save()
        {
       
        }

        public void Reboot()
        {
            OnTarget = false;
        }
    }
}
