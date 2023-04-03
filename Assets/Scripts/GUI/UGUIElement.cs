using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UGUIElement : MonoBehaviour
{
    [SerializeField] private KeyBinds.Binds key;
    [SerializeField] private Camera cam;
    [SerializeField] private float maxDistance = 3f;

    private Transform target;
    private RectTransform element;
    private TextMeshProUGUI label;

    private event Action<bool> onTargetUpdate;

    private bool isActive;

    private void Awake()
    {
        element = GetComponent<RectTransform>();
        label = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        label.text = KeyBinds.Instance.Keys[key].ToString();
    }

    private void Update()
    {
        if (target != null && IsCloseEnough())
        {
            Vector2 screenPoint = cam.WorldToScreenPoint(target.position);
            element.position = screenPoint;
            label.enabled = true;

            if (!isActive){
                isActive = true;
                onTargetUpdate?.Invoke(true);
            }
        }
        else
        {
            label.enabled = false;

            if (isActive){
                isActive = false;
                onTargetUpdate?.Invoke(false);
            }
        }
    }

    private bool IsCloseEnough() => Vector3.Distance(cam.transform.position, target.position) < maxDistance;

    public void Subscribe(Action<bool> onTargetUpdate) => this.onTargetUpdate += onTargetUpdate;

    public void AddTarget(Transform target)
    {
        this.target = target;
    }

    public void RemoveTarget(Transform target)
    {
        this.target = null;
    }
}
