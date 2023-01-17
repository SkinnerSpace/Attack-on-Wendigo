using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    private LayerMask ignoreLayers = ~(1 << 13 | 1 << 14);

    private Camera cam;
    public bool hasTarget { get; private set; }
    public RaycastHit Target => target;

    private RaycastHit target;
    

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update() => Observe();

    private void Observe()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out target, Mathf.Infinity, ignoreLayers);
        hasTarget = target.transform != null;
    }
}
