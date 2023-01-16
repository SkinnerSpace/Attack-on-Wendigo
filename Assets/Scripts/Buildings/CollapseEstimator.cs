using UnityEngine;

public class CollapseEstimator : MonoBehaviour
{
    private const float TIME_PER_METER = 0.1f;
    private const float FREQUENCY_PER_SECOND = 10f;

    [SerializeField] private MeshRenderer mesh;

    public float height => propSize.y; 
    private Vector3 propSize;

    public float time { get; private set; }
    public float frequency { get; private set; }

    public void UpdateEstimations()
    {
        propSize = mesh.bounds.size;
        time = propSize.y * TIME_PER_METER;
        frequency = time * FREQUENCY_PER_SECOND;
    }
}

