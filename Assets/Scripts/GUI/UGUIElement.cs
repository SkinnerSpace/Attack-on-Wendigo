using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UGUIElement : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private KeyBinds.Binds key;
    [SerializeField] private float maxDistance = 3f;

    private Camera cam;
    private Transform target;
    private RectTransform element;
    private TextMeshProUGUI label;

    private event Action<bool> onTargetUpdate;

    private void Awake()
    {
        element = GetComponent<RectTransform>();
        label = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        cam = player.Data.Cam;
        label.text = KeyBinds.Instance.Keys[key].ToString();
        player.GetController<InteractionController>().Subscribe(AddTarget, RemoveTarget);
    }

    private void Update()
    {
        if (target != null && IsCloseEnough())
        {
            Vector2 screenPoint = cam.WorldToScreenPoint(target.position);
            element.position = screenPoint;
            label.enabled = true;
        }
        else
        {
            label.enabled = false;
        }
    }

    private bool IsCloseEnough() => Vector3.Distance(player.transform.position, target.position) < maxDistance;

    public void Subscribe(Action<bool> onTargetUpdate) => this.onTargetUpdate += onTargetUpdate;

    private void AddTarget(Transform target)
    {
        this.target = target;
        onTargetUpdate(true);
    }

    private void RemoveTarget(Transform target)
    {
        this.target = null;
        onTargetUpdate(false);
    }
}
