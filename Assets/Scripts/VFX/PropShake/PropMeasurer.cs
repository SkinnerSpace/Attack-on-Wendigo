using UnityEngine;

public class PropMeasurer : MonoBehaviour
{
    [SerializeField] Transform prop;
    private MeshRenderer mesh;

    public float Width => size.x;
    public float Height => size.y;
    public float Length => size.z;
    public Vector3 size { get; private set; }

    public Vector3 realSize;
    public Vector3 realScale;

    public void Measure()
    {
        mesh = prop.GetComponent<MeshRenderer>();
        size = mesh.bounds.size;

        realSize = mesh.bounds.size;
        realScale = prop.lossyScale;
    }
}

