using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoCorpseCollector : MonoBehaviour
    {
        private const int MAX_CORPSES = 1;

        public static WendigoCorpseCollector Instance { get; private set; }

        private Stack<WendigoCorpse> corpses;

        private void Awake()
        {
            Instance = this;
        }

        public void AddCorpse(WendigoCorpse corpse){
            if (corpses == null){
                corpses = new Stack<WendigoCorpse>();
            }

            corpses.Push(corpse);

            if (corpses.Count >= MAX_CORPSES){
                WendigoCorpse rottenCorpse = corpses.Pop();
                rottenCorpse.Bury(); 
            }
        }
    }
}

