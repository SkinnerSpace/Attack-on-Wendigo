using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HitBoxManager : MonoBehaviour
{
    [SerializeField] private Transform hitBoxesRoot;

    [SerializeField] private List<SurfaceData> surfaceData;
    [Header("Surface data")]
    [SerializeField] private List<SurfaceData> fleshSurfaceData;
    [SerializeField] private List<SurfaceData> boneSurfaceData;
    private Dictionary<SurfaceTypes, List<SurfaceData>> surfaces;

    [Header("Settings")]
    [SerializeField] private bool resetSurfaceData;
    [SerializeField] private Transform owner;

    public HitBoxProxy[] hits;

    private void OnEnable()
    {
        surfaces = new Dictionary<SurfaceTypes, List<SurfaceData>>();
        surfaces.Add(SurfaceTypes.Flesh, fleshSurfaceData);
        surfaces.Add(SurfaceTypes.Bone, boneSurfaceData);

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
            {
                surface.Set(surfaces[surface.SurfaceType]);
            }
        }
    }

    public void ResetState()
    {
        foreach (HitBoxProxy hitBox in hits){
            hitBox.ResetState();
        }
    }
}




