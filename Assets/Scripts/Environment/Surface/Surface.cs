using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField]
    private SurfaceTypes type = SurfaceTypes.Undefined;
    public SurfaceTypes Type => type;
}
