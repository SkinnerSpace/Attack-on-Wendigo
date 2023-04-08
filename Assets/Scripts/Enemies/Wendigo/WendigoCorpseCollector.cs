using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoCorpseCollector : MonoBehaviour
    {
        [SerializeField] private int maxCorpses = 3;

        public static WendigoCorpseCollector Instance { get; private set; }

        private Queue<WendigoCorpse> corpses;

        private void Awake()
        {
            Instance = this;
        }

        public void AddCorpse(WendigoCorpse corpse){
            if (corpses == null){
                corpses = new Queue<WendigoCorpse>();
            }

            corpses.Enqueue(corpse);

            if (corpses.Count >= maxCorpses){
                WendigoCorpse rottenCorpse = corpses.Dequeue();
                rottenCorpse.PrepareToFuneral(); 
            }
        }
    }
}

