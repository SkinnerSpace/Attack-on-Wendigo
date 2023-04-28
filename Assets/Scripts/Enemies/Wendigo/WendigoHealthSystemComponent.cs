using System;
using UnityEngine;

namespace WendigoCharacter
{
    public class WendigoHealthSystemComponent : MonoBehaviour, IHealthSystem
    {
        private WendigoHealthSystem healthSystem;

        private void Start()
        {
            Wendigo wendigo = GetComponent<Wendigo>();
            healthSystem = wendigo.GetController<WendigoHealthSystem>();
        }

        public void SubscribeOnDeath(Action onDeath)
        {
            if (healthSystem != null){
                healthSystem.SubscribeOnDeath(onDeath);
            }
        }

        public void RestoreHealth(int health)
        {
            if (healthSystem != null){
                healthSystem.RestoreHealth(health);
            }
        }
    }
}