using System.Collections.Generic;
using UnityEngine;

public class TrajectoryMarker : MonoBehaviour
{
    [SerializeField] private TrajectoryRenderer projector;

    private ProjectedBody body;
    private Trajectory trajectory;

    private void Awake()
    {
        trajectory = new Trajectory();
        body = new ProjectedBody();
    }

    public void DrawTrajectory(Vector3 position, Vector3 velocity, Vector3 acceleration)
    {
        body.Set(position, velocity, acceleration);
        trajectory.CalculateFor(body);
        projector.Render(trajectory);
    }

    public void CancelTrajectory() => projector.DisableRendering();
}
