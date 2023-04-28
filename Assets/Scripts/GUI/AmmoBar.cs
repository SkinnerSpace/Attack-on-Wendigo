using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    private GUIAnimator animator;

    private void Awake()
    {
        animator = GetComponent<GUIAnimator>();
    }

    private void Start()
    {
        PlayerEvents.current.onAmmoUpdate += OnUpdate;
        PlayerEvents.current.onWeaponThrown += Deactivate;
    }

    public void OnUpdate(int ammo)
    {
        label.text = ammo.ToString();
        animator.PlayPush();
    }

    public void Deactivate(){
        label.text = 0.ToString();
    }
}
