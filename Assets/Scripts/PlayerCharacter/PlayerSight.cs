using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    private LayerMask ignoreLayers = ~(1 << 13 | 1 << 14);

    private Camera cam;
    public bool hasTarget { get; private set; }
    public RaycastHit Spot => spot;
    private RaycastHit spot;

    private List<Type> targetTypes = new List<Type>();

    public event Action<bool> notifyOnTarget;

    private void Awake()
    {
        InitializeComponents();
        InitializeTargets();
    }

    private void Start() => SubscribeEventListeners();
    private void Update() => Observe();

    private void InitializeComponents()
    {
        cam = GetComponent<Camera>();
    }

    private void InitializeTargets()
    {
        targetTypes.Add(typeof(IPickable));
        targetTypes.Add(typeof(IDamageable));
    }

    private void SubscribeEventListeners()
    {
        notifyOnTarget += Aim.Instance.SetOnTarget;
    }

    private void Observe()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out spot, Mathf.Infinity, ignoreLayers);
        hasTarget = spot.transform != null;

        UpdateTargetStatus();
    }

    private void UpdateTargetStatus()
    {
        bool suitable = hasTarget ? IsSuitable(spot.transform) : false;
        notifyOnTarget(suitable);
    }

    private bool IsSuitable(Transform targetTransform)
    {
        foreach (Type type in targetTypes)
            if (targetTransform.GetComponent(type) != null)
                return true;

        return false;
    }
}

// Make an item picker class
// Make a target object + null target object
// Send target object to the item picker
// Item picker can pick up an item if it's not of a nullt type, otherwise get message that there is nothing to pick up
// Pickable item should disappear after being picked up
