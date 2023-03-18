using System;

namespace Character
{
    public abstract class BaseController
    {
        public abstract void Initialize(PlayerCharacter main);
        public abstract void Connect();
        public abstract void Disconnect();
    }
}
