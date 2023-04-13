using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoBar : MonoBehaviour, IAmmoObserver
{
    [SerializeField] private TextMeshProUGUI label;

    public static AmmoBar Instance;

    private GUIAnimator animator;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<GUIAnimator>();
    }

    public void OnUpdate(int ammo)
    {
        label.text = ammo.ToString();
        animator.PlayPush();
    }

    public void SetActive(bool active)
    {
        if (!active){
            label.text = 0.ToString();
        }
    }
}
