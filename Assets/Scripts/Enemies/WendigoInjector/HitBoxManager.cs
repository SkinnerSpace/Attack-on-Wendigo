using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HitBoxManager : MonoBehaviour
{
    [SerializeField] private Transform hitBoxesRoot;
    [SerializeField] private List<SurfaceData> surfaceData;
    [SerializeField] private bool resetSurfaceData;

    [SerializeField] private Transform owner;

    public HitBoxProxy[] hits;

    private void OnEnable()
    {
        FindHitBoxes();
        ProvideHitBoxesWithOwner();
        AddSurfaceComponentsIfNecessary();
    }

    private void FindHitBoxes()
    {
        if (hitBoxesRoot != null)
            hits = hitBoxesRoot.GetComponentsInChildren<HitBoxProxy>();
    }

    private void ProvideHitBoxesWithOwner()
    {
        if (owner != null)
        {
            foreach (HitBoxProxy hitBox in hits)
            {
                hitBox.SetOwner(owner);
            }
        }
    }

    private void AddSurfaceComponentsIfNecessary()
    {
        foreach (HitBoxProxy hitBox in hits)
        {
            Surface surface = hitBox.transform.GetComponent<Surface>();

            if (surface == null){
                surface = hitBox.gameObject.AddComponent<Surface>();
            }

            if (resetSurfaceData)
                surface.Set(surfaceData);
        }
    }
}

