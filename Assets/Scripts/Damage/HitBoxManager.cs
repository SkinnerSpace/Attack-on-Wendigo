using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class HitBoxManager : MonoBehaviour
{
    [SerializeField] private Transform hitBoxesRoot;
    [SerializeField] private WendigoFleshSurfaceData surfaceData;

    public HitBoxProxy[] hits;

    private void OnEnable()
    {
        FindSurfaceComponents();
        AddSurfaceComponentsIfNecessary();
    }

    private void FindSurfaceComponents()
    {
        if (hitBoxesRoot != null)
            hits = hitBoxesRoot.GetComponentsInChildren<HitBoxProxy>();
    }

    private void AddSurfaceComponentsIfNecessary()
    {
        foreach (HitBoxProxy hitBox in hits)
        {
            Surface surface = hitBox.transform.GetComponent<Surface>();

            if (surface == null){
                surface = hitBox.gameObject.AddComponent<Surface>();
            }

            surface.Set(surfaceData.particleName, surfaceData.sFXData);
        }
    }
}
