using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoCorpse : MonoBehaviour
    {
        [SerializeField] private RagDollController ragdoll;

        public void Bury()
        {
            Debug.Log("Bury");
            ragdoll.SwitchOff();
            OFF
                // Switch off, shake and DISAPPEAR with a blood puddle!
        }
    }
}

