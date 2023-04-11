﻿using UnityEngine;
using WendigoCharacter;


[ExecuteAlways]
public class CorpseCollisionController : MonoBehaviour
{
    [SerializeField] private Transform collidersRoot;
    [SerializeField] private WendigoData data;
    [SerializeField] private bool visualize;
    [SerializeField] private ShakeSettings shake;

    public bool Visualize => visualize;
    public CorpseGroundCollider[] groundColliders;
    
    public void SwitchOn()
    {
        foreach (CorpseGroundCollider groundCollider in groundColliders)
            groundCollider.SwitchOn();
    }

    public void SwitchOff()
    {
        foreach (CorpseGroundCollider groundCollider in groundColliders)
            groundCollider.SwitchOff();
    }

# if UNITY_EDITOR
    private void OnEnable()
    {
        FindColliders();
        SetData();
    }
# endif

    private void FindColliders()
    {
        if (collidersRoot != null)
            groundColliders = collidersRoot.GetComponentsInChildren<CorpseGroundCollider>();
    }

    private void SetData()
    {
        if (data != null)
        {
            foreach (CorpseGroundCollider groundCollider in groundColliders)
                groundCollider.Initialize(this, data, shake);
        }
    } 
}

