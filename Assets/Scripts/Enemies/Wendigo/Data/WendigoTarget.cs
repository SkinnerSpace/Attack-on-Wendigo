using UnityEngine;
using System;

namespace WendigoCharacter
{
    [Serializable]
    public class WendigoTarget
    {
        public Transform transform;
        public Vector3 Position => character.Position;
        public bool IsGrounded => character.IsGrounded;
        public bool Exist { get; private set; }
        public ICharacterData character;

        public void Set(ICharacterData character, Transform transform)
        {
            this.character = character;
            this.transform = transform;

            Exist = true;
        }
    }
}
