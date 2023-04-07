using UnityEngine;

public class SharedSurface : MonoBehaviour
{
    [SerializeField] private SharedSurfaceData sharedData;
    private Surface surface;

    private void Awake()
    {
        surface = GetComponent<Surface>();
        surface.Set(sharedData.data);
    }
}

