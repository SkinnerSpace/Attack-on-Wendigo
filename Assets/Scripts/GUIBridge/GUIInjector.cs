using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class GUIInjector : MonoBehaviour
{
    [SerializeField] private UGUIElement uGUIElement;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private AmmoBar ammoBar;
    [SerializeField] private PlayerCharacter player;

    private void Start()
    {
        player.GetController<InteractionController>().Subscribe(uGUIElement.AddTarget, uGUIElement.RemoveTarget);
    }
}
