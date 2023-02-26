using UnityEngine;

public class VisionRaycast
{
    private Camera cam;
    private LayerMask layerMask;

    public VisionRaycast(Camera cam, LayerMask layerMask)
    {
        this.cam = cam;
        this.layerMask = layerMask;
    }

    public Transform Cast(Vector2 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        Transform currentTarget = null;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            currentTarget = hit.transform;

        return currentTarget;
    }
}
