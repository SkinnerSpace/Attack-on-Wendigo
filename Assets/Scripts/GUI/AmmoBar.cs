using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoBar : MonoBehaviour
{
    private TextMeshProUGUI label;
    private Animator animator;

    private static int updateAnimation = Animator.StringToHash("Update");
    private static int emptyAnimation = Animator.StringToHash("Empty");

    public static AmmoBar Instance;

    private void Awake()
    {
        Instance = this;
        label = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    public void UpdateAmmo(int ammo)
    {
        label.text = ammo.ToString();
        EnforcedPlayAnimation(updateAnimation);
    }

    public void UpdateOutOfAmmo() => EnforcedPlayAnimation(emptyAnimation);

    public void SetActive(bool active) => label.enabled = active;

    private void PlayAnimation(int animation) => animator.Play(animation);

    private void EnforcedPlayAnimation(int animation) => animator.Play(animation, -1, 0f);
}
