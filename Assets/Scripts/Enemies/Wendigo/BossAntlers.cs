using System.Collections.Generic;
using UnityEngine;

namespace WendigoCharacter
{
    public class BossAntlers : MonoBehaviour, ISwitchable
    {
        [SerializeField] private List<Limb> smallAntlers;
        [SerializeField] private List<HitBoxProxy> smallAntlersHitBoxes;

        public void SwitchOn()
        {
            gameObject.SetActive(true);
            DisableSmallAntlers();
        }

        private void DisableSmallAntlers()
        {
            foreach (Limb smallAntler in smallAntlers)
            {
                smallAntler.Hide();
            }

            foreach (HitBoxProxy hitBox in smallAntlersHitBoxes)
            {
                hitBox.SwitchOff();
            }
        }

        public void SwitchOff()
        {
            gameObject.SetActive(false);
        }
    }
}

