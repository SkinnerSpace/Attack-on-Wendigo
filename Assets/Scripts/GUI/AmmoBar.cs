using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoBar : MonoBehaviour, IAmmoObserver
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Animator animator;

    private static int updateAnimation = Animator.StringToHash("Update");

    public static AmmoBar Instance;

    private void Awake() => Instance = this;

    public void OnUpdate(int ammo)
    {
        label.text = ammo.ToString();
        EnforcedPlayAnimation(updateAnimation);
    }

    public void SetActive(bool active) => label.enabled = active;

    private void PlayAnimation(int animation) => animator.Play(animation);

    private void EnforcedPlayAnimation(int animation) => animator.Play(animation, -1, 0f);
}
