using UnityEngine;

public class CollapseEstimator
{
    private const float TIME_PER_METER = 0.2f;
    private const float FREQUENCY_PER_SECOND = 10f;

    public float height => propSize.y; 
    public Vector3 propSize { get; private set; }

    public float time { get; private set; }
    public float frequency { get; private set; }

    public void EstiamteFor(CollapseAcceptor acceptor)
    {
        propSize = acceptor.mesh.bounds.size;
        time = propSize.y * TIME_PER_METER;
        frequency = time * FREQUENCY_PER_SECOND;
    }
}

