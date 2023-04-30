using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UGUIElement : MonoBehaviour
{
    [SerializeField] private KeyActions key;
    [SerializeField] private Camera cam;
    [SerializeField] private float maxDistance = 3f;

    private RaycastHit targetHit;
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
        label.text = KeyBinds.Instance.keyActionPairs[key].ToString();
        PlayerEvents.current.onInteractiveTargetAdded += AddTarget;
        PlayerEvents.current.onInteractiveTargetRemoved += RemoveTarget;
    }

    private void Update()
    {
        if (target != null && SurfaceIsCloseEnough())
        {
            Vector2 screenPoint = cam.WorldToScreenPoint(targetHit.transform.position);
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

    private bool SurfaceIsCloseEnough() => Vector3.Distance(cam.transform.position, targetHit.point) < maxDistance;

    public void Subscribe(Action<bool> onTargetUpdate) => this.onTargetUpdate += onTargetUpdate;

    public void AddTarget(RaycastHit targetHit)
    {
        this.targetHit = targetHit;
        target = targetHit.transform;
    }

    public void RemoveTarget()
    {
        target = null;
    }
}
