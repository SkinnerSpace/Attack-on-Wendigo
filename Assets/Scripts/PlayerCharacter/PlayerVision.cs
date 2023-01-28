﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    private const float REACH_DISTANCE = 4f;

    [SerializeField] private ItemHolder weaponHolder;

    private Camera cam;
    public bool hasTarget { get; private set; }
    public RaycastHit Spot => spot;
    private RaycastHit spot;
    private bool suitable;

    public Vector3 point { get; private set; }

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

        Physics.Raycast(ray, out spot, Mathf.Infinity, ComplexLayers.Vision);
        hasTarget = spot.transform != null;

        UpdateTargetStatus();
        SpotAnItem();

        point = hasTarget ? spot.point : ray.direction * 300f;
    }

    private void UpdateTargetStatus()
    {
        suitable = hasTarget ? IsSuitable(spot.transform) : false;
        notifyOnTarget(suitable); 
    }

    private void SpotAnItem()
    {
        if (IsAbleToTakeAnItem()){
            if (InputReader.interact) weaponHolder.TakeAnItem(spot.transform);
        }
    }

    private bool IsAbleToTakeAnItem() => suitable &&
                                         GetDistanceTo(spot) <= REACH_DISTANCE &&
                                         GetTargetType(spot.transform) == typeof(IPickable);

    private float GetDistanceTo(RaycastHit spot) => Vector3.Distance(spot.transform.position, transform.position);

    private bool IsSuitable(Transform targetTransform)
    {
        Type type = GetTargetType(targetTransform);
        return type != null;
    }

    private Type GetTargetType(Transform targetTransform)
    {
        foreach (Type type in targetTypes){
            if (targetTransform.GetComponent(type) != null)
                return type;
        }

        return null;
    }
}
